using System.ComponentModel.DataAnnotations;

namespace Netrex.Frontend.Application.ViewModels.ProductManagement
{
    public class ProductsVm
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Discount { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string ImagePublicId { get; set; } = string.Empty; 
        public bool IsPrimary { get; set; }
        public DateTime UploadedAt { get; set; }
    }
    public class CloudinaryUploadResult
    {
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
    }
}
