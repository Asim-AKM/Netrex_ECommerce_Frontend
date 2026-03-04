using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Application.Services.WishList;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;
using Netrex.Frontend.Application.ViewModels.ProductManagement;
using Netrex.Frontend.Application.ViewModels.WishList;

namespace Netrex.Frontend.Blazor.Components.Pages
{
    public partial class Home
    {
        [Inject] private ToastService _Toast { get; set; } = default!;
        [Inject] private ICartItemManager CartItemManager { get; set; } = default!;

        private List<ProductsVm> products = new();
        private bool isLoading = true;

        VmAddCartItem model = new VmAddCartItem();

        protected override async Task OnInitializedAsync()
        {
            if (products.Count == 0)
                await LoadProductsAsync();
            await LoadWishedProductsAsync();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // await LoadProductsAsync();
                await JS.InvokeVoidAsync("initializeHomePage");
            }
        }
        private async Task LoadProductsAsync()
        {
            try
            {
                isLoading = true;
                var response = await ProductManager.GetAllProductsAsync();
                if (response.IsSuccess)
                {
                    products = response.Data ?? new();
                }
                else
                {
                    Console.WriteLine($"Failed to load products: {response.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
            finally
            {
                isLoading = false;
            }
        }
        private void BuyNow(Guid productId)
        {

            Navigation.NavigateTo($"/ProductlandingPage", new NavigationOptions
            {

            });

        }

        public async Task AddToCart()
        {
            var response = await CartItemManager.AddCartItemAsync(model);
            if (!response.IsSuccess)
                _Toast.Error(response.Message);
        }
        private void AddToCart(Guid productId)
        {
            // Product Id CartIteam module developer necha deya gya line sa la sakta hain
            Console.WriteLine($"Product {productId} added to cart.");

        }


        #region WishedList Logic
        [Inject] private WishListStateService WishListState { get; set; } = default!;
        [Inject] private IWishListManager WishListManager { get; set; } = default!;

        private readonly Guid _userId = Guid.Parse("4818cc53-f71a-4bfe-97f0-2453268a22a0");
        private Dictionary<Guid, Guid> _wishedProducts = new();
        private HashSet<Guid> _loadingWishIds = new();

        private async Task LoadWishedProductsAsync()
        {
            var response = await WishListManager.GetWishListItemsAsync(_userId);
            if (response.IsSuccess && response.Data != null)
            {
                _wishedProducts = response.Data
                    .ToDictionary(x => x.ProductId, x => x.WishListItemId);
            }
        }
        private async Task ToggleWishList(Guid productId)
        {
            _loadingWishIds.Add(productId);
            StateHasChanged();

            if (_wishedProducts.ContainsKey(productId))
            {
                var wishListItemId = _wishedProducts[productId];
                var response = await WishListManager.DeleteWishListItemAsync(wishListItemId);

                if (response.IsSuccess)
                {
                    _wishedProducts.Remove(productId); 
                    WishListState.Decrement();
                    _Toast.Success("Removed from wishlist!");
                }
                else
                {
                    _Toast.Error(response.Message);
                }
            }
            else
            {
                var request = new VmAddWishListItem(productId, _userId);
                var response = await WishListManager.AddWishListItemAsync(request);

                if (response.IsSuccess && response.Data != Guid.Empty)
                {
                    _wishedProducts[productId] = response.Data;
                    WishListState.Increment();
                    _Toast.Success("Added to wishlist!");
                }
                else
                {
                    _Toast.Error(response.Message);
                }
            }

            _loadingWishIds.Remove(productId);
            StateHasChanged();
        }
        #endregion
    }
}