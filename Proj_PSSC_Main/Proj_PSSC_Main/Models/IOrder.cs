using Proj_PSSC_Main.Services;
namespace Proj_PSSC_Main.Models;

public interface IOrder: IServiceReturnType
{
    
}

public record NonExistingOrder() : IOrder;