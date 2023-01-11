
namespace Proj_PSSC_1.Models;

public record Order (string id, Product[] products) : IOrder;
