using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.DTO_s.ProductDto
{
    public class UpdateProductDto
    {
        [property: JsonPropertyName("productId")]
        public Guid ProductId { get; set; }
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
        [property: JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }
        [property: JsonPropertyName("uploadedAt")]
        public DateTime UploadedAt { get; set; }
        public List<string> DeletedImagePublicIds { get; set; } = new();
        public List<ImageDto> Images { get; set; } = new();
    }
    public class ImagesDto
    {
        public string ImageUrl { get; set; }
        public string CloudPublicId { get; set; }
        public bool IsPrimary { get; set; }
    }
}
