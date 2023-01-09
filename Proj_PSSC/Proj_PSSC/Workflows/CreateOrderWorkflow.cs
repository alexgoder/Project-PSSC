using System.Collections;
using System.Text.Json;
using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;
using Proj_PSSC.Models;
using Proj_PSSC.Services;

namespace Proj_PSSC.Workflows;

public class CreateOrderWorkflow
{
    public readonly Product[] reqProducts;
    public readonly IServiceGetData serviceGetProducts;
    public readonly IServiceGetData serviceGetOrders;
    public CreateOrderWorkflow(Product[] products, IServiceGetData serviceGetProducts, IServiceGetData serviceGetOrders)
    {
        this.reqProducts = products;
        this.serviceGetProducts = serviceGetProducts;
        this.serviceGetOrders = serviceGetOrders;
    }

    public Option<IOrder> ExecuteAsync()
    {
        Either<IProduct, IOrder> exp = from products in getProds()
                                     from filteredProds in filterProds(products)
                                     select (IOrder)this.GenerateOrder(filteredProds);

        return exp.ToOption();

    }

    private Either<IProduct, Product[]> filterProds(Product[] prods)
    {
        List<Product> filteredProds = new List<Product> { };
        bool listOk = true;
        for(int clientItem = 0; clientItem < this.reqProducts.Length; clientItem++)
        {
            bool itemExists = false;

            for(int internItem = 0; internItem < prods.Length; internItem++)
            {
                if (this.reqProducts[clientItem].id == prods[internItem].id)
                {
                    itemExists = true;
                    if(this.reqProducts[clientItem].qty > prods[internItem].qty)
                    {
                        if(prods[internItem].qty<=0)
                        {
                            return Left<IProduct, Product[]>(new OutOfStockProduct());
                        }
                        return Left<IProduct, Product[]>(new InsufficientStockProduct(prods[internItem].qty));
                    }
                }
            }
            if (itemExists == false)
                return Left<IProduct, Product[]>(new NonExistingProduct());
        }

        return Right<IProduct, Product[]>(this.reqProducts);
    }


    private Either<IProduct, Product[]> getProds()
    {
        Product[] stockProds = (Product[])this.serviceGetProducts.GetData();
        if (stockProds == null || stockProds.Length <= 0)
            return Left<IProduct, Product[]>(new NotFoundProduct("Products could not be found"));
        return Right<IProduct, Product[]>(stockProds);
    }


    private string GenerateOrderId()
    {
        Random random = new Random();
        int a;
        string id = "";

        for(int i = 0; i < 5; i++)
        {
            a = random.Next();
            id += a.ToString();
        }
        return id;
    }

    private IOrder GenerateOrder(Product[] prods)
    {
        string generatedOrderId = this.GenerateOrderId();
        Order[] existingOrders = (Order[])this.serviceGetOrders.GetData();
        for(int i=0;i<existingOrders.Length;i++)
        {
            if (existingOrders[i].id==generatedOrderId)
            {
                generatedOrderId = this.GenerateOrderId();
                i = 0;
            }
        }

        return new Order(generatedOrderId,prods);
    }
}