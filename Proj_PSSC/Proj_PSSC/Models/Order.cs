
namespace Proj_PSSC.Models;

public record Order:IOrder
{
    public string id;
    public Product[] products;
    public Order(string id, Product[] products)
    {
        this.id = id;
        this.products = products;
    }
    
    
    
}