using System;
using System.Collections;
using System.Text.Json;
using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;
using Proj_PSSC_1.Models;
using Proj_PSSC_1.Services;

namespace Proj_PSSC_1.Workflows;

public class DeleteOrderWorkflow
{
    public readonly IOrder orderToDelete;
   
    public readonly IServiceGetData serviceGetOrders;

    public DeleteOrderWorkflow(string id, IServiceGetData serviceGetOrders )
    {
        this.serviceGetOrders = serviceGetOrders;
        var res = FindOrderById(id);
        if (res.IsSome)
            this.orderToDelete = (Order)res;
        else
            this.orderToDelete = new NonExistingOrder();
        
    }

    public Option<IOrder> Execute()
    {
        var result = from orders in getOrders()
                     from fileteredOrders in filterOrders(orders)
                     select this.orderToDelete;
         return result.ToOption();
        
    }

    private Either<IOrder, Order[]> filterOrders(Order[] orders)
    {
        
        List<Order> updatedOrders = new List<Order> { };
        bool orderExists = false;
        if(this.orderToDelete is Order){
            Order order = (Order)this.orderToDelete;
            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i].id != order.id)
                {
                    updatedOrders.Add(orders[i]);
                }
                else
                {
                    orderExists = true;
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
