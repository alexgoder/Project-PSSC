namespace Proj_PSSC.Models;

public record Product(string name,int id,int qty,double price):IProduct;