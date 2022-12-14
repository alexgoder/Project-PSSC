using System.Collections;
using System.Text.Json;
using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;
using Proj_PSSC.Models;

namespace Proj_PSSC.Workflows;

public class CreateOrderWorkflow
{
    public readonly IProduct[] products;
    public CreateOrderWorkflow(IProduct[]products)
    {
        string str = File.ReadAllText("/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Proj_PSSC/TestFiles/Products.json");
        Product[] product = JsonSerializer.Deserialize<Product[]>(str);
        List<Product> prods = product.ToList();
        var variable = from prod in prods where prod!=null select prod;

        foreach (var shit in variable)
        {
            Console.WriteLine(shit);
        }
        this.products = products;
    }

    public  Option<IOrder> ExecuteAsync()
    {
        Either<string,IOrder> exp = from products in getProds()
            from filteredProds in filterById(123,products)
            select (IOrder)new Order(123,filteredProds);
                    
        return exp.ToOption() ;

    }

    public Either<string, Product[]> filterById(int id, Product[] prods)
    {
        
        return Right<string,Product[]>(new Product[0]);
    }
    public Either<string, Product[]> getProds()
    {
        return Right<string,Product[]>(new Product[0]);
    } 
}