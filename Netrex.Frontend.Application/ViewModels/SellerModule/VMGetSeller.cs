using System.Runtime;
using System.Text.Json.Serialization;

namespace Netrex.Frontend.Application.ViewModels.SellerModule
{
    public class VMGetSeller
    {
        [JsonPropertyName("SellerId")]
        public Guid SellerId { get; set; }
        
        [JsonPropertyName("StoreName")]
        public string StoreName { get; set; } = "";

        [JsonPropertyName("StoreDescription")]
        public string Description { get; set; } = "";

        [JsonPropertyName("Address")]

        public string Address { get; set; } = "";

        [JsonPropertyName("Status")]
        public string Status { get; set; } = "Pending";
    }
}
