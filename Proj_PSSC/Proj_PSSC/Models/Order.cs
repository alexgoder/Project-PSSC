
namespace Proj_PSSC.Models;



public record Order(string id, Product[] products) :IOrder;

