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
        [Inject]
        private IProductManager ProductManageres { get; set; } = default!;

        private List<ProductsVm> products = new();
        private bool isLoading = true;
        string errorMessage="";
        VmAddCartItem model = new VmAddCartItem();
        private List<VmProductCategory> categories=new();
        private bool isLoadingCategories = false;
        private int currentPage = 1;
        private int pageSize = 10;


        protected override async Task OnInitializedAsync()
        {
            await LoadProductsAsync();
            await LoadProductCategory();
        }
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
                            "Clothing"=> "/assets/icons/icons8-bag-100.png",
                            "Home & Kitchen" => "/assets/icons/small-appliance.png",
                            "Electronics" => "/assets/icons/responsive.png",
                            "Books" => "/assets/icons/icons8-books-100.png",
                            "Sports" => "/assets/icons/sports.png",
                            "Kids" => "/assets/icons/kids.png",
                            _ => "/assets/icons/default.png",
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
        private async Task LoadProductsAsync(Guid? categoryId = null)
        {
            try
            {
                isLoading = true;
                //var response = await ProductRanking.GetBestSellersAsync();
                //        //var response = await ProductRanking.GetNewArrivalsAsync();
                //        //var response = await ProductRanking.GetTopRatedAsync();
                //        //var response = await ProductRanking.GetTrendingAsync();
                var response = await ProductRanking
                    .GetHomepageProductsAsync(categoryId, currentPage, pageSize);

                if (response.IsSuccess)
                {
                    products = response.Data ?? new();
                }
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
            var response = await ProductRanking.GetHomepageProductsAsync(null, nextPage, pageSize);

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

                var response = await ProductRanking.GetHomepageProductsAsync(null, prevPage, pageSize);

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
