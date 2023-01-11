using System;
using System.Collections;
using System.Text.Json;
using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;
using Proj_PSSC_2.Models;
using Proj_PSSC_2.Services;

namespace Proj_PSSC_2.Workflows;

public class ModifyOrderWorkflow
{
    public readonly IOrder orderToModify;

    public readonly string[] prodsToDelete;
   
    public readonly IServiceGetData serviceGetOrders;

    public ModifyOrderWorkflow(string orderId, string[] prodIdToDelete,IServiceGetData serviceGetOrders )
    {
        this.serviceGetOrders = serviceGetOrders;
        var res = FindOrderById(orderId);
        if (res.IsSome)
            this.orderToModify = (Order)res;
        else
            this.orderToModify = new NonExistingOrder();

        this.prodsToDelete = prodIdToDelete;
        
    }

    public Option<IOrder> Execute()
    {
        var result = from orders in getOrders()
                     from fileteredOrders in filterOrders(orders)
                     select this.orderToModify;
         return result.ToOption();
        
    }

    private Either<IOrder, Order[]> filterOrders(Order[] orders)
    {
        
        List<Order> updatedOrders = new List<Order> { };
        List<Product> updatedProds = new List<Product> { };
        bool orderExists = false;
        if(this.orderToModify is Order){
            Order order = (Order)this.orderToModify;
            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i].id != order.id)
                {
                    updatedOrders.Add(orders[i]);
                }
                else
                {
                    int counter = 0;
                    orderExists = true;
                    for(int j = 0; j < orders[i].products.Length;j++)
                    {
                        for(int k=0;k<this.prodsToDelete.Length;k++)
                        {
                            if (this.prodsToDelete[k] == orders[i].products[j].id)
                            {
                                counter++;
                            }
                            else
                            {
                                updatedProds.Add(orders[i].products[j]);
                            }
                        }
                    }

                    if (counter == this.prodsToDelete.Length)
                    {
                        updatedOrders.Add(new Order(orders[i].id, updatedProds.ToArray()));
                    }
                    else
                        return Left<IOrder, Order[]>(new OrderCouldNotModify());
                }
            }
            if(orderExists==true)
            {
                this.serviceGetOrders.SetData(updatedOrders.ToArray());
                return Right<IOrder, Order[]>(updatedOrders.ToArray());
            }
            
        }
        
        return Left<IOrder, Order[]>(new NonExistingOrder());
    }

    private Either<IOrder, Order[]> getOrders()
    {
        Order[] orders = (Order[])this.serviceGetOrders.GetData();
        if (orders == null || orders.Length <= 0)
            return Left<IOrder, Order[]>(new NotFoundOrders("Orders could not be found"));
        return Right<IOrder, Order[]>(orders);
    }

    public Option<Order> FindOrderById(string id)
    {
        Order[] orders = (Order[])this.serviceGetOrders.GetData();
        for (int i = 0; i < orders.Length; i++)
            if (orders[i].id == id)
                return Some<Order>(orders[i]);
        return None;
    }

    
}
