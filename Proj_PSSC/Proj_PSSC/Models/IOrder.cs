using Proj_PSSC.Services;

namespace Proj_PSSC.Models;

public interface IOrder: IServiceReturnType
{
    
}

public record NonExistingOrder() : IOrder;