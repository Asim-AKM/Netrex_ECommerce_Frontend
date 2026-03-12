using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Order;

namespace Netrex.Frontend.Application.Services.CartAndOrder.Interfaces
{
    public interface IOrderManager
    {
        Task<ApiResponse<List<VmGetOrder>>> vmGetOrders();
    }
}
