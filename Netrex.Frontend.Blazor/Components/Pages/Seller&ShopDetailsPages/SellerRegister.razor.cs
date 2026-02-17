using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.SellerAndShop.Interfaces;
using Netrex.Frontend.Application.ViewModels.SellerModule;

namespace Netrex.Frontend.Blazor.Components.Pages.Seller_ShopDetailsPages
{
    public partial class SellerRegister
    {
        [Inject] public IShopManager _shopManager { get; set; } = default!;
        [Inject] public ISellerManager _sellerManager { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;

        public List<VmShopDetail> ShopDetails { get; set; } = new();
        public VmSeller seller { get; set; } = new();

        private bool isLoadingCategories = false;
        private bool categoriesLoaded = false;

        // Lazy load categories when dropdown is clicked (on focus)
        private async Task LoadCategoriesAsync()
        {
            if (categoriesLoaded) return; // Already loaded, no DB call

            isLoadingCategories = true;

            var shops = await _shopManager.GetAllShopsAsync();

            if (shops != null)
            {
                ShopDetails = shops;
                categoriesLoaded = true;
            }

            isLoadingCategories = false;
        }

        private async Task HandleRegistration()
        {
            var result = await _sellerManager.CreateSellerAsync(seller);

            if (result != null)
            {
                Navigation.NavigateTo("/seller/plan");
            }
        }
    }
}
