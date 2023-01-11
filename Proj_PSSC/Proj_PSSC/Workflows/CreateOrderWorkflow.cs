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
    public CreateOrderWorkflow(string[] products, IServiceGetData serviceGetProducts, IServiceGetData serviceGetOrders)
    {
        
        this.serviceGetProducts = serviceGetProducts;
        this.serviceGetOrders = serviceGetOrders;
        this.reqProducts = GetProducts(products);
    }

    public Option<IOrder> Execute()
    {
        Either<IProduct, IOrder> exp = from products in getProds()
                                     from filteredProds in filterProds(products)
                                     select (IOrder)this.GenerateOrder(filteredProds);

        var result=exp.ToOption();

        if (result.IsSome)
            this.serviceGetOrders.SetData(new List<Order> { (Order)result }.ToArray());

        return result;

    }

    private Either<IProduct, Product[]> filterProds(Product[] prods)
    {
        List<Product> filteredProds = new List<Product> { };
        bool listOk = true;
        for(int clientItem = 0; clientItem < this.reqProducts.Length; clientItem++)
        {
            bool itemExists = false;

            if(this.reqProducts[clientItem].qty<=0)
                return Left<IProduct, Product[]>(new ProductListQtyIsNegative("Invalid quantity"));

            for (int internItem = 0; internItem < prods.Length; internItem++)
            {
                if (this.reqProducts[clientItem].id == prods[internItem].id && this.reqProducts[clientItem].name == prods[internItem].name)
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
                    Product prod = new Product(prods[internItem].name, prods[internItem].id, this.reqProducts[clientItem].qty, Math.Round((this.reqProducts[clientItem].qty * prods[internItem].price), 2));

                    filteredProds.Add(prod);
                }
            }
            if (itemExists == false)
                return Left<IProduct, Product[]>(new NonExistingProduct());
        }

        return Right<IProduct, Product[]>(filteredProds.ToArray());
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
            a = random.Next()%10;
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
        this.serviceGetProducts.SetData(prods);
        return new Order(generatedOrderId,prods);
    }

    private Product[] GetProducts(string[] product)
    {
        List<Product> prods = new List<Product> { };
        Product[] stockProds = (Product[])this.serviceGetProducts.GetData();
        for (int i=0;i<product.Length;i+=2)
        {
            for(int j=0;j<stockProds.Length;j++)
                if (stockProds[j].id == product[i] && int.Parse(product[i+1])>=0)
                {
                    prods.Add(new Product(stockProds[j].name, stockProds[j].id, int.Parse(product[i + 1]), stockProds[j].price));
                }
        }

        return prods.ToArray();
    }
}