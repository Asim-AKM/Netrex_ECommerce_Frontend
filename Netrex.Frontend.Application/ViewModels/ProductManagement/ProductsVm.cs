using System.Text.Json.Serialization;

namespace Netrex.Frontend.Application.ViewModels.ProductManagement
{
    public class ProductsVm
    {
        [property: JsonPropertyName("productId")]
        public Guid ProductId { get; set; }
        [property: JsonPropertyName("sellerId")]
        public Guid SellerId { get; set; }
        [property: JsonPropertyName("productName")]
        public string ProductName { get; set; } = string.Empty;
        [property: JsonPropertyName("productDescription")]
        public string ProductDescription { get; set; } = string.Empty;
        [property: JsonPropertyName("categoryName")]
        public string CategoryName { get; set; } = string.Empty;
        [property: JsonPropertyName("price")]
        public double Price { get; set; }
        [property: JsonPropertyName("discount")]
        public double Discount { get; set; }
        [property: JsonPropertyName("stockQuantity")]
        public int StockQuantity { get; set; }
        [property: JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [property: JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; } = string.Empty;
        [property: JsonPropertyName("cloudPublicId")]
        public string CloudPublicId { get; set; } = string.Empty;
        public List<CloudinaryUploadResult> Images { get; set; } = new();
        [property: JsonPropertyName("uploadedAt")]
        public DateTime UploadedAt { get; set; }
        public bool IsPrimary { get; set; }
        public List<string> DeletedImagePublicIds { get; set; } = new();

        public double FinalPrice => Price - (Price * Discount / 100);
        public string FormattedFinalPrice => $"Rs. {FinalPrice:N0}";
        public string FormattedOriginalPrice => $"Rs. {Price:N0}";
        public string DiscountDisplay => Discount > 0 ? $"{Discount}% OFF" : "";
    }
    public class CloudinaryUploadResult
    {
        [JsonPropertyName("imageUrl")]
        public string? Url { get; set; }
        [JsonPropertyName("cloudPublicId")]
        public string? CloudPublicId { get; set; }
        [property: JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }
    }
}
