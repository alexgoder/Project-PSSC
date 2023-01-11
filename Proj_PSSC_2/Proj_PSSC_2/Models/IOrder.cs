using Proj_PSSC_2.Services;

namespace Proj_PSSC_2.Models;

public interface IOrder : IServiceReturnType
{

}

public record NonExistingOrder():IOrder;

public record NotFoundOrders(string msg):IOrder;

public record OrderCouldNotModify():IOrder;