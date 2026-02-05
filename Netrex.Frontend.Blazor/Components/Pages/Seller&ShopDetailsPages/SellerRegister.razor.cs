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
        public List<VmShopDetail> ShopDetails { get; set; } = new();
        public SellerRegister(IShopManager shopManager, ISellerManager sellerManager)
        {
            _shopManager = shopManager;
            _sellerManager = sellerManager;
        }


        // Constructor ko khatam kar dein agar zarurat nahi, Inject attribute kafi hai

        protected override async Task OnInitializedAsync()
        {
            // Data fetch ho raha hai
            var shop = await _shopManager.GetAllShopsAsync();
            if (shop != null)
            {
                ShopDetails = shop;
            }
        }
        public async Task<string> CreateSeller(VmSeller vmSeller)
        {
            return await _sellerManager.CreateSellerAsync(vmSeller);
        }


        SellerRegisterModel seller = new();
        void CreateStore()
        {
            // Seller details handle karne ke liye (Module 3)
            Console.WriteLine($"Store {seller.StoreName} Created for Category {seller.CategoryName}");
        }

        public class SellerRegisterModel
        {
            public string StoreName { get; set; }
            public string StoreDescription { get; set; }
            public string CategoryName { get; set; }
            public string Address { get; set; }
        }
    }
}
