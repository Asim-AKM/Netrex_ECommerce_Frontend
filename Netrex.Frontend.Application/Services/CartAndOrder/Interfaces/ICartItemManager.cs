using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.Services.CartAndOrder.Interfaces
{
    public  interface ICartItemManager
    {
        Task<ApiResponse<bool>> AddCartItemAsync(VmAddCartItem vm);
        Task<ApiResponse<bool>> IncreaseQuantityAsync(Guid cartitemid);
        Task<ApiResponse<bool>> DecreaseQuantityAsync(Guid cartItemId);
        Task<ApiResponse<bool>> RemoveItemAsync(Guid cartitemid);
    }
}
