using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Netrex.Frontend.Application.Services.SellerAndShop.Interfaces;
using Netrex.Frontend.Application.ViewModels.SellerModule;
using System.ComponentModel.DataAnnotations;

namespace Netrex.Frontend.Blazor.Components.Pages.Seller_ShopDetailsPages
{
    public partial class SellerRegister
    {
        [Inject] public IShopManager _shopManager { get; set; } = default!;
        [Inject] public ISellerManager _sellerManager { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;

        public List<VmShopDetail> ShopDetails { get; set; } = new();
        public SellerRegisterModel seller { get; set; } = new();

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
            if (!Guid.TryParse(seller.ShopId, out Guid shopGuid))
                return;

            var newSeller = new VmSeller
            {
                StoreName = seller.StoreName,
                StoreDescription = seller.StoreDescription,
                ShopId = shopGuid,
                StoreAddress = seller.Address,
                UserId = Guid.NewGuid() // replace with logged-in user
            };

            var result = await _sellerManager.CreateSellerAsync(newSeller);

            if (result != null)
            {
                Navigation.NavigateTo("/seller/plan");
            }
        }

        public class SellerRegisterModel
        {
            [Required(ErrorMessage = "Store name is required")]
            [MinLength(3, ErrorMessage = "Minimum 3 characters required")]
            public string StoreName {  get; set; } = string.Empty;

            [Required(ErrorMessage = "Store description is required")]
            [MinLength(10, ErrorMessage = "Minimum 10 characters required")]
            public string StoreDescription { get; set; } = string.Empty;

            [Required(ErrorMessage = "Shop category is required")]
            public string ShopId { get; set; } = string.Empty;

            [Required(ErrorMessage = "Store address is required")]
            public string Address { get; set; } = string.Empty;
        }
    }
}
