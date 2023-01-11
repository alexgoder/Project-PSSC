namespace Proj_PSSC_1.Models;

public record Product(string name, string id, int qty, double price=0) : IProduct;