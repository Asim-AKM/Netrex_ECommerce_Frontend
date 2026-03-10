using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Commons.Mappers.Order;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Order;

namespace Netrex.Frontend.Blazor.Components.Pages.ProdcutsAndDeliveryPage
{
    public partial class OrderSummary
    {
        [Inject]
        public ICartItemManager CartService { get; set; } = default!;

        private List<VmOrderSummary> OrderItems = new();

        private double SubTotal;

        protected override async Task OnInitializedAsync()
        {
            var response = await CartService.GetCartItemAsync();

            if (response.IsSuccess && response.Data != null)
            {
                OrderItems = OrderSummaryMapper.Map(response.Data);

                SubTotal = OrderItems.Sum(x => x.total);
            }
        }
    }
}