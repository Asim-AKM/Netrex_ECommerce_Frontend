using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class VmSeller
{
    [JsonPropertyName("SellerId")]
    public Guid SellerId { get; set; }

    [Required(ErrorMessage = "User is required")]
    [JsonPropertyName("UserId")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "Shop category is required")]
    [JsonPropertyName("ShopId")]
    public Guid ShopId { get; set; }

    [Required(ErrorMessage = "Store name is required")]
    [MinLength(3, ErrorMessage = "Store name must be at least 3 characters")]
    [JsonPropertyName("StoreName")]
    public string StoreName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Store description is required")]
    [MinLength(10, ErrorMessage = "Description must be at least 10 characters")]
    [JsonPropertyName("StoreDescription")]
    public string StoreDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "Store address is required")]
    [JsonPropertyName("Address")]
    public string StoreAddress { get; set; } = string.Empty;
}
