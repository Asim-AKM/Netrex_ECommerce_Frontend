using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Application.Services.ProductManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;
using Netrex.Frontend.Application.ViewModels.ProductManagement;

namespace Netrex.Frontend.Blazor.Components.Pages
{
    public partial class Home
    {
        [Inject]
        private ICartItemManager CartItemManager { get; set; } = default!;
        [Inject]
        private ToastService _Toast { get; set; } = default!;
        [Inject]
        private IProductRanking ProductRanking { get; set; } = default!;

        private List<ProductsVm> products = new();
        private bool isLoading = true;
        string errorMessage;
        VmAddCartItem model = new VmAddCartItem();

        protected override async Task OnInitializedAsync() => await LoadProductsAsync();

        private async Task LoadProductsAsync()
        {
            try
            {
                isLoading = true;
                //var response = await ProductRanking.GetBestSellersAsync();
                //var response = await ProductRanking.GetNewArrivalsAsync();
                //var response = await ProductRanking.GetTopRatedAsync();
                //var response = await ProductRanking.GetTrendingAsync();
                var response = await ProductRanking.GetHomepageProductsAsync();

                if (response.IsSuccess)
                {
                    products = response.Data ?? new();
                }
                else
                {
                    errorMessage = response.Message;
                    products = new();
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Something went wrong while loading products.";
                products = new();
            }
            finally
            {
                isLoading = false;
            }
        }


        private void AddToCart(Guid productId)
        {
            // Product Id CartIteam module developer necha deya gya line sa la sakta hain
            Console.WriteLine($"Product {productId} added to cart.");

        }

        private void BuyNow(Guid productId)
        {

            Navigation.NavigateTo($"/ProductlandingPage", new NavigationOptions
            {

            });

        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JS.InvokeVoidAsync("initializeHomePage");
            }
        }

        public async Task AddToCart()
        {

            var response = await CartItemManager.AddCartItemAsync(model);

            if (!response.IsSuccess)
            {
                _Toast.Error(response.Message);
            }
        }
    }
}
