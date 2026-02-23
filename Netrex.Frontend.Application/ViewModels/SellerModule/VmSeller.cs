using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class VmSeller
{
    [JsonPropertyName("SellerId")]
    public Guid SellerId { get; set; }

    [JsonPropertyName("UserId")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "Shop category is required")]
    [JsonPropertyName("ShopId")]
    public Guid ShopId { get; set; }


    [Required(ErrorMessage = "Store name is required")]
    [MinLength(3, ErrorMessage = "Minimum 3 characters required")]
    [JsonPropertyName("StoreName")]
    public string StoreName { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int TotalProducts { get; set; }
    public decimal TotalRevenue { get; set; }

    [Required(ErrorMessage = "Store description is required")]
    [MinLength(10, ErrorMessage = "Minimum 10 characters required")]
    [JsonPropertyName("StoreDescription")]
    public string StoreDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "Store address is required")]
    [JsonPropertyName("Address")]
    public string StoreAddress { get; set; } = string.Empty;
}
