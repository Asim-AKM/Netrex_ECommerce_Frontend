using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Application.Services.WishList;
using Netrex.Frontend.Application.ViewModels.WishList;

namespace Netrex.Frontend.Blazor.Components.Pages.WishListPages
{
    public partial class WishListPage
    {
        [Inject] private IWishListManager WishListManager { get; set; } = default!;
        [Inject] private ToastService ToastService { get; set; } = default!;
        [Inject] private WishListStateService WishListState { get; set; } = default!;

        private bool isLoading = true;
        private bool isDeleting = false;
        private bool ShowPopup = false;
        private Guid? itemToDeleteId = null;
        private Guid? currentUserId = null;

        private List<VmGetWishListItem> Products = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Products.Count == 0)
                await LoadWishList();
        }

        private async Task LoadWishList()
        {
            isLoading = true;
            var response = await WishListManager.GetWishListItemsAsync(CurrentUserId);
            HandleApiResponse(response);
            if (response.IsSuccess)
                Products = response.Data ?? new();
            else
                ToastService.Info(response.Message);

            isLoading = false;
        }

        private void AskDelete(Guid id)
        {
            itemToDeleteId = id;
            ShowPopup = true;
        }

        private void HidePopup()
        {
            ShowPopup = false;
            itemToDeleteId = null;
        }

        private async Task ConfirmDelete()
        {
            if (itemToDeleteId == null) return;
            isDeleting = true;
            var response = await WishListManager.DeleteWishListItemAsync(itemToDeleteId.Value);
            if (response.IsSuccess)
            {
                Products.RemoveAll(x => x.WishListItemId == itemToDeleteId.Value);
                WishListState.Decrement();
            }
            else
                ToastService.Error(response.Message);

            isDeleting = false;
            HidePopup();
        }
    }
}