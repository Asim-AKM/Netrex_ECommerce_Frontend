using System.Text.Json.Serialization;

namespace Netrex.Frontend.Application.DTO_s.ProductDto
{
    public record AddProductDto
        (
        [property: JsonPropertyName("productName")] string ProductName,
        [property: JsonPropertyName("productDescription")] string ProductDescription,
        [property: JsonPropertyName("categoryName")] string CategoryName,
        [property: JsonPropertyName("price")] double Price,
        [property: JsonPropertyName("discount")] double Discount,
        [property: JsonPropertyName("stockQuantity")] int StockQuantity,
        [property: JsonPropertyName("createdAt")] DateTime CreatedAt,
        [property: JsonPropertyName("imageUrl")] string ImageUrl,
        [property: JsonPropertyName("cloudPublicId")] string CloudPublicId,
        [property: JsonPropertyName("isPrimary")] bool IsPrimary,
        [property: JsonPropertyName("uploadedAt")] DateTime UploadedAt
        );
  
}
