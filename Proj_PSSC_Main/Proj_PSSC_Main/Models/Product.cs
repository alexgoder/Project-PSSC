namespace Proj_PSSC_Main.Models;

public record Product(string name,string id,int qty,double price=0):IProduct;
