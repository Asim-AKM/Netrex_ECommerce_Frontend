using System.Text.Json.Serialization;

namespace Netrex.Frontend.Application.ViewModels.SellerModule
{
    public class VmShopDetail
    {
        [JsonPropertyName("shopDetailsId")]
        public Guid ShopId { get; set; }
        [JsonPropertyName("CategoryName")]
        public string ShopName { get; set; } = string.Empty;
    }
}
