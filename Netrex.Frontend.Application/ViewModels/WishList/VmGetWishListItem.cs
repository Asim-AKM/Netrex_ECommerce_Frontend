using System.Text.Json.Serialization;
namespace Netrex.Frontend.Application.ViewModels.WishList
{
    public record VmGetWishListItem
    (
        [property: JsonPropertyName("wishListItemId")] Guid WishListItemId,
        [property: JsonPropertyName("productId")] Guid ProductId,
        [property: JsonPropertyName("imgeId")] Guid ImgeId,
        [property: JsonPropertyName("imageUrl")] string? ImageUrl,
        [property: JsonPropertyName("cloudPublicId")] string? CloudPublicId,
        [property: JsonPropertyName("sellerId")] Guid SellerId,
        [property: JsonPropertyName("productName")] string ProductName,
        [property: JsonPropertyName("productDescription")] string ProductDescription,
        [property: JsonPropertyName("price")] double Price,
        [property: JsonPropertyName("discount")] double Discount
    );
}
