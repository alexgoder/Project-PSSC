namespace Proj_PSSC_Main.Models;


public record Order(string id, Product[] products) :IOrder;

