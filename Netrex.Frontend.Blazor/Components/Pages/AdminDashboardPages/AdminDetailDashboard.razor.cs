using Domain_Service.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Netrex.Frontend.Application.Commons.Enums;
using Netrex.Frontend.Application.Services.ProductManagement.Interfaces;
using Netrex.Frontend.Application.Services.SellerAndShop.Interfaces;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.PaymentAndPayOutManagement;
using Netrex.Frontend.Application.ViewModels.ProductManagement;
using Netrex.Frontend.Application.ViewModels.UserManagement;

namespace Netrex.Frontend.Blazor.Components.Pages.AdminDashboardPages
{
    public partial class AdminDetailDashboard
    {
        [Inject] public required IJSRuntime JSRuntime { get; set; }
        [Inject] public required IUserManager UserManager { get; set; }
        [Inject] public required NavigationManager Navigation { get; set; }
        [Inject] public required ISellerManager SellerManager { get; set; }
        [Inject] public required IProductManager ProductManager { get; set; }

        // Fields for user management
        private List<VmUser> Users = new();
        private bool IsUserLoading = true;
        private string? StatusMessage;
        private bool ShowStatusModal = false;
        private VmUser? PendingStatusUser;
        private UserStatus PendingNewStatus;
        private UserStatus PreviousStatus;
        private bool IsSuccess = false;

        // Product Management
        private List<ProductsVm> Products = new(); // Initialize to avoid null
        private bool IsProductsLoading = true;

        protected override async Task OnInitializedAsync()
        {
            // Concurrent loading start kar sakte hain performance ke liye
            await Task.WhenAll(LoadUsers(), LoadSellers(), LoadProducts());
        }

        private async Task LoadProducts()
        {
            try
            {
                IsProductsLoading = true;
                var response = await ProductManager.GetAllProductsAsync(CurrentUserId);

                if (response != null && response.IsSuccess && response.Data != null)
                {
                    Products = response.Data;
                }
                else
                {
                    Products = new List<ProductsVm>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
                Products = new List<ProductsVm>();
            }
            finally
            {
                IsProductsLoading = false;
                StateHasChanged(); // UI update lazmi hai
            }
        }

        // --- User Management Methods ---
        private async Task LoadUsers()
        {
            IsUserLoading = true;
            var response = await UserManager.GetUsersAsync();
            if (response.IsSuccess) Users = response.Data ?? new();
            IsUserLoading = false;
        }

        private void OnStatusDropdownChanged(VmUser user, ChangeEventArgs e)
        {
            if (Enum.TryParse<UserStatus>(e.Value?.ToString(), out var newStatus))
            {
                PendingStatusUser = user;
                PreviousStatus = user.Userstatus;
                PendingNewStatus = newStatus;
                ShowStatusModal = true;
            }
        }

        private async Task ConfirmStatusChange()
        {
            if (PendingStatusUser == null) return;
            ShowStatusModal = false;
            var response = await UserManager.UpdateUserStatusAsync(PendingStatusUser.Id, PendingNewStatus);
            IsSuccess = response.IsSuccess;
            StatusMessage = response.IsSuccess ? "User status updated successfully!" : response.Message;
            if (response.IsSuccess) await LoadUsers();
            else PendingStatusUser.Userstatus = PreviousStatus;
            StateHasChanged();
            await Task.Delay(3000);
            StatusMessage = string.Empty;
            StateHasChanged();
        }

        private void CancelStatusChange()
        {
            if (PendingStatusUser != null) PendingStatusUser.Userstatus = PreviousStatus;
            ShowStatusModal = false;
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender) await JSRuntime.InvokeVoidAsync("ntxNavigation.init");
        }

        // --- Seller Management ---
        private List<VmSeller> Sellers = new();
        private bool IsSellersLoading = true;
        private VmSellerPayout? SelectedPayout;
        private bool ShowPayoutModal = false;
        private string? PayoutMessage;
        private bool IsPayoutSuccess = false;

        private async Task LoadSellers()
        {
            IsSellersLoading = true;
            var response = await SellerManager.GetSellerAsync();
            if (response.IsSuccess) Sellers = response.Data ?? new();
            IsSellersLoading = false;
        }

        private async Task ViewPayout(Guid sellerId)
        {
            var response = await SellerManager.GetSellerPayoutByIdAsync(sellerId);
            SelectedPayout = response.IsSuccess ? response.Data : new VmSellerPayout
            {
                SellerId = sellerId,
                PaymentStatus = PaymentStatus.pending,
                AmountToPay = 0
            };
            ShowPayoutModal = true;
        }

        private async Task MarkAsPaid(Guid sellerPayoutId)
        {
            var response = await SellerManager.UpdateSellerPayoutAsPaidAsync(sellerPayoutId);
            IsPayoutSuccess = response.IsSuccess;
            PayoutMessage = response.IsSuccess ? "Payout marked as paid successfully!" : response.Message;
            ShowPayoutModal = false;
            StateHasChanged();
            await Task.Delay(3000);
            PayoutMessage = string.Empty;
            StateHasChanged();
        }
    }
}