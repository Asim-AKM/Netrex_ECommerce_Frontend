using Domain_Service.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement;

namespace Netrex.Frontend.Blazor.Components.Pages.AdminDashboardPages
{
    public partial class AdminDetailDashboard
    {
        [Inject] public required IJSRuntime JSRuntime { get; set; }
        [Inject] public required IUserManager UserManager { get; set; }
        [Inject] public required NavigationManager Navigation { get; set; }

        private List<VmUser> Users = [];
        private bool IsUserLoading = true;
        private string? StatusMessage;

        private bool ShowStatusModal = false;
        private VmUser? PendingStatusUser;
        private UserStatus PendingNewStatus;
        private UserStatus PreviousStatus;
        private bool IsSuccess = false;


        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            IsUserLoading = true;
            var response = await UserManager.GetUsersAsync();
            if (response.IsSuccess)
                Users = response.Data;
            IsUserLoading = false;
        }

        //private async Task HandleStatusChange(Guid userId, ChangeEventArgs e)
        //{
        //    if (Enum.TryParse<UserStatus>(e.Value?.ToString(), out var newStatus))
        //    {
        //        var response = await UserManager.UpdateUserStatusAsync(userId, newStatus);
        //        StatusMessage = response.IsSuccess ? "Status updated successfully" : response.Message;

        //        if (response.IsSuccess)
        //            await LoadUsers();

        //        StateHasChanged();
        //    }
        //}

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

            if (response.IsSuccess)
                await LoadUsers();
            else
                PendingStatusUser.Userstatus = PreviousStatus;

            StateHasChanged();

            // Auto-hide after 3 seconds
            await Task.Delay(3000);
            StatusMessage = string.Empty;
            StateHasChanged();
        }

        private void CancelStatusChange()
        {
            if (PendingStatusUser != null)
                PendingStatusUser.Userstatus = PreviousStatus; // revert dropdown

            ShowStatusModal = false;
            StateHasChanged();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await JSRuntime.InvokeVoidAsync("ntxNavigation.init");
        }
    }
}