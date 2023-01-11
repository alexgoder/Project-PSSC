using Proj_PSSC_1.Services;

namespace Proj_PSSC_1.Models;

public interface IOrder : IServiceReturnType
{

}

public record NonExistingOrder():IOrder;

public record NotFoundOrders(string msg):IOrder;