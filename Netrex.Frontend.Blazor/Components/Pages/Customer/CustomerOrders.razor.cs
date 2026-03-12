using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Order;
using Netrex.Frontend.Application.Services.Common;

namespace Netrex.Frontend.Blazor.Components.Pages.Customer
{
    public partial class CustomerOrders
    {
        [Inject]
        private IOrderManager OrderManager { get; set; } = default!;

        [Inject]
        private ToastService _Toast { get; set; } = default!;

        private List<VmGetOrder> orders = new();

        private bool IsLoading = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadOrdersAsync();
        }

        private async Task LoadOrdersAsync()
        {
            try
            {
                IsLoading = true;

                var response = await OrderManager.vmGetOrders();

                if (!response.IsSuccess)
                {
                    _Toast.Error(response.Message ?? "Failed to load orders");
                    return;
                }

                orders = response.Data ?? new List<VmGetOrder>();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ViewDetails(Guid orderId)
        {
            Console.WriteLine($"OrderId: {orderId}");

            // yahan baad me method call karna:
            // await OrderManager.GetOrderItems(orderId);
        }
    }
}