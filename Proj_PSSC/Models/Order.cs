using Proj_PSSC.Exceptions;

namespace Proj_PSSC.Models;

public record Order:IOrder
{
    public int id;
    public Product[] products;
    public int qty = 0;
    public Order(int id, Product[] products)
    {
        this.id = id;
        this.products = products;

        if (this.qty > 0)
        {
            this.qty = products.Length;
        }
        else
        {
            throw new EmptyProductListException();
        }
    }
    
    
    
}