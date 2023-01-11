using Proj_PSSC_2.Services;

namespace Proj_PSSC_2.Models;

public interface IProduct : IServiceReturnType
{

}

public record NonExistingProduct() : IProduct;

public record InsufficientStockProduct(int availableQty) : IProduct;

public record OutOfStockProduct() : IProduct;

public record NotFoundProduct(string msg) : IProduct;

public record ProductListQtyIsNegative(string msg) : IProduct;