using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Netrex.Frontend.Application.Services.Customer.Interfaces;
using Netrex.Frontend.Application.Services.SellerAndShop.Interfaces;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Blazor.DTOs;

namespace Netrex.Frontend.Blazor.Components.Pages.AdminDashboardPages
{
    public partial class AdminDetailDashboard
    {
        [Inject] public required IJSRuntime JSRuntime { get; set; }
        [Inject] public required IUserManager UserManager { get; set; }
        [Inject] public required ICustomerManager CustomerManager { get; set; }
        [Inject] public required ISellerManager SellerManager { get; set; }

        [Inject] public required NavigationManager Navigation { get; set; }
        private List<GetUsersDto> Users = [];
        private bool IsUserLoading = true;
        private List<VmSeller> Sellers = []; 
        private bool IsSellersLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
            await LoadSellers();
        }

        private async Task LoadUsers()
        {
            IsUserLoading = true;
            var response = await UserManager.GetUsersAsync();
            if (response.IsSuccess)
            {
                Users = response.Data;
            }

            IsUserLoading = false;
        }

        private async Task LoadSellers()
        {
            IsSellersLoading = true;

            try
            {
                Sellers = await SellerManager.GetSellerAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sellers: {ex.Message}");
            }
            finally
            {
                IsSellersLoading = false;
                StateHasChanged();
            }
        }

        private void ViewSeller(Guid sellerId)
        {
            Navigation.NavigateTo($"/seller-details/{sellerId}");
        }

        private void EditSeller(VmSeller seller)
        {
            Navigation.NavigateTo($"/edit-seller/{seller.SellerId}");
        }
        private void EditUser(GetUsersDto user)
        {
            Navigation.NavigateTo($"/admin/edit-user/{user.Id}");
        }
        private async Task HandleDelete(Guid userId)
        {
            var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this user?");
            if (confirmed)
            {
                var response = await UserManager.DeleteUserAsync(userId);
                if (response.IsSuccess)
                {
                    await LoadUsers();
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("ntxNavigation.init");
            }
        }
    }
}