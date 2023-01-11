
namespace Proj_PSSC_2.Models;

public record Order (string id, Product[] products) : IOrder;
