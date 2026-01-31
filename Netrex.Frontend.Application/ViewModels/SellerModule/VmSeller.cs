using System.Text.Json.Serialization;

namespace Netrex.Frontend.Application.ViewModels.SellerModule
{
    public class VmSeller
    {
        [JsonPropertyName("SellerId")]
        public Guid SellerId { get; set; }
        [JsonPropertyName("UserId")]
        public Guid UserId { get; set; }
        [JsonPropertyName("ShopId")]
        public Guid ShopId { get; set; }
        [JsonPropertyName("StoreName")]
        public string StoreName { get; set; } = string.Empty;
        [JsonPropertyName("StoreDescription")]
        public string StoreDescription { get; set; } = string.Empty;
        [JsonPropertyName("Address")]
        public string StoreAddress { get; set; }   = string.Empty;
    }

}

