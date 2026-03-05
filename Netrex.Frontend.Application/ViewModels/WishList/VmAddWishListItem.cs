using System.Text.Json.Serialization;

namespace Netrex.Frontend.Application.ViewModels.WishList
{
    public record VmAddWishListItem(
        [property: JsonPropertyName("productId")] Guid ProductId,
        [property: JsonPropertyName("userId")] Guid UserId
    );
}