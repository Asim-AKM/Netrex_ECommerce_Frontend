using System.Text.Json.Serialization;

namespace Netrex.Frontend.Application.DTO_s.ProductDto
{
    public class AddProductDto()
    {
        public Guid SellerId { get; set; } 
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Discount { get; set; }
        public int StockQuantity { get; set; }
        public List<ImageDto> Images { get; set; } = new();
    }
    public class ImageDto
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string CloudPublicId { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
    }

}
