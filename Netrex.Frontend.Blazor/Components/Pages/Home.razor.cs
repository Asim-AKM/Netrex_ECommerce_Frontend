using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Application.Services.WishList;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;
using Netrex.Frontend.Application.ViewModels.ProductManagement;
using Netrex.Frontend.Application.ViewModels.WishList;
using Netrex.Frontend.Application.Services.ProductManagement.Interfaces;

namespace Netrex.Frontend.Blazor.Components.Pages
{
    public partial class Home
    {
        #region Injections
        // ── Injections ──────────────────────────────────────────
        [Inject] private ICartItemManager CartItemManager { get; set; } = default!;
        [Inject] private ToastService _Toast { get; set; } = default!;
        [Inject] private IProductRanking ProductRanking { get; set; } = default!;
        [Inject] private IProductManager ProductManageres { get; set; } = default!;
        [Inject] private WishListStateService WishListState { get; set; } = default!;
        [Inject] private IWishListManager WishListManager { get; set; } = default!;
        #endregion
          
        #region State
        // ── State ───────────────────────────────────────────────
        private List<ProductsVm> products = new();
        private List<VmProductCategory> categories = new();
        private bool isLoading = true;
        private bool isLoadingCategories = false;
        private string errorMessage = "";
        private int currentPage = 1;
        private int pageSize = 10;
        VmAddCartItem model = new VmAddCartItem();
        #endregion
          
        #region Lifecycle
        // ── Lifecycle ───────────────────────────────────────────
        protected override async Task OnInitializedAsync()
        {
            await LoadProductsAsync();
            await LoadProductCategory();
            await LoadWishedProductsAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await JS.InvokeVoidAsync("initializeHomePage");
        }
        #endregion
          
        #region Categories
        // ── Categories ──────────────────────────────────────────
        private async Task LoadProductCategory()
        {
            isLoadingCategories = true;
            try
            {
                var response = await ProductManageres.GetCategoriesAsync();
                if (response != null && response.Data != null)
                {
                    categories = response.Data.Select(c => new VmProductCategory
                    {
                        Id = c.Id,
                        ProductCategoryName = c.ProductCategoryName,
                        IconUrl = c.ProductCategoryName switch
                        {
                            "Clothing"       => "/assets/icons/icons8-bag-100.png",
                            "Home & Kitchen" => "/assets/icons/small-appliance.png",
                            "Electronics"    => "/assets/icons/responsive.png",
                            "Books"          => "/assets/icons/icons8-books-100.png",
                            "Sports"         => "/assets/icons/sports.png",
                            "Kids"           => "/assets/icons/kids.png",
                            _                => "/assets/icons/default.png",
                        }
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                isLoadingCategories = false;
            }
        }
        #endregion

        #region Products
        // ── Products ────────────────────────────────────────────
        private async Task LoadProductsAsync(Guid? categoryId = null)
        {
            try
            {
                isLoading = true;
                var response = await ProductRanking
                    .GetHomepageProductsAsync(categoryId, currentPage, pageSize);

                if (response.IsSuccess)
                    products = response.Data ?? new();
                else
                {
                    _Toast.Info("No more products to display.");
                    errorMessage = response.Message;
                    products = new();
                }
            }
            catch
            {
                errorMessage = "Something went wrong while loading products.";
                products = new();
            }
            finally
            {
                isLoading = false;
            }
        }

        private async Task NextPage()
        {
            int nextPage = currentPage + 1;
            var response = await ProductRanking
                .GetHomepageProductsAsync(null, nextPage, pageSize);

            if (response.IsSuccess && response.Data != null && response.Data.Any())
            {
                products = response.Data;
                currentPage = nextPage;
            }
            else
            {
                _Toast.Info("No more products to display.");
            }
        }

        private async Task PreviousPage()
        {
            if (currentPage > 1)
            {
                int prevPage = currentPage - 1;
                var response = await ProductRanking
                    .GetHomepageProductsAsync(null, prevPage, pageSize);

                if (response.IsSuccess && response.Data != null && response.Data.Any())
                {
                    products = response.Data;
                    currentPage = prevPage;
                }
                else
                {
                    _Toast.Info("No products on previous page.");
                }
            }
        }

        private async Task OnCategoryChanged(Guid categoryId)
        {
            currentPage = 1;
            await LoadProductsAsync(categoryId);
        }
        #endregion
          
        #region Cart
        // ── Cart ────────────────────────────────────────────────
        public async Task AddToCart()
        {
            var response = await CartItemManager.AddCartItemAsync(model);
            if (!response.IsSuccess)
                _Toast.Error(response.Message);
        }

        private void AddToCart(Guid productId)
        {
            Console.WriteLine($"Product {productId} added to cart.");
        }

        private void BuyNow(Guid productId)
        {
            Navigation.NavigateTo($"/ProductlandingPage", new NavigationOptions { });
        }
        #endregion
        
        #region WishList
        // ── WishList ────────────────────────────────────────────
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