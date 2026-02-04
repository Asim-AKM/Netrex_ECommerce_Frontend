using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.SellerAndShop.Interfaces;
using Netrex.Frontend.Application.ViewModels.SellerModule;

namespace Netrex.Frontend.Blazor.Components.Pages.Seller_ShopDetailsPages
{
    public partial class SellerRegister
    {
        [Inject]
        public IShopManager _shopManager { get; set; } = default!;

        [Inject]
        public ISellerManager _sellerManager { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;
        public List<VmShopDetail> ShopDetails { get; set; } = new();

        [SupplyParameterFromForm]
        public SellerRegisterModel seller { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var shop = await _shopManager.GetAllShopsAsync();
            if (shop != null)
            {
                ShopDetails = shop;
            }
        }
        private async Task HandleRegistration()
        {
            bool isSaved = false;
            try
            {
                var newSeller = new VmSeller
                {
                    StoreName = seller.StoreName,
                    StoreDescription = seller.StoreDescription,
                    ShopId = Guid.Parse(seller.ShopId),
                    StoreAddress = seller.Address,
                };

                var result = await _sellerManager.CreateSellerAsync(newSeller);

                if (result != null)
                {
                    Console.WriteLine("Data Save Successfully");
                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            if (isSaved)
            {
                Navigation.NavigateTo("/seller/plan");
            }
        }

        public class SellerRegisterModel
        {

            public string StoreName { get; set; } = string.Empty;

            public string StoreDescription { get; set; } = string.Empty;

            public string ShopId { get; set; } = string.Empty;

            public string Address { get; set; } = string.Empty;

            public string CategoryName { get; set; } = string.Empty;
        }
    }
}
