using System.Text.Json.Serialization;

namespace Netrex.Frontend.Blazor.Models.Seller_ShopDetailsModels
{
    public class VMSeller
    {
        [JsonPropertyName("sellerid")]
        public Guid SellerId { get; set; }
        [JsonPropertyName("userid")]
        public Guid UserId { get; set; }
        [JsonPropertyName("shopid")]
        public Guid ShopId { get; set; }
        [JsonPropertyName("storename")]
        public string? StoreName { get; set; }
        [JsonPropertyName("storedescription")]
        public string? StoreDescription { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
       
    }
 
}
