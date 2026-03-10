using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Order;

namespace Netrex.Frontend.Application.Commons.Mappers.Order
{
    public static class OrderSummaryMapper
    {
        public static List<VmOrderSummary> Map(this List<VmGetCartItem> vmGetCartItem)
        {
             return vmGetCartItem.Select(x => new VmOrderSummary
             {
                    ProductName= x.ProductName,
                    Quantity = x.Quantity,
                    price = x.Price,
                    total = x.Price * x.Quantity
             }).ToList();
        }
    }
}
