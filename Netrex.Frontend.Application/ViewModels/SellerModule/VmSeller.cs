using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.ViewModels.SellerModule
{
    public class VmSeller
    {
        [JsonPropertyName("storeName")]
        public string StoreName { get; set; } = string.Empty;
        [JsonPropertyName("storeDescription")]
        public string StoreDescription { get; set; } = string.Empty;
        [JsonPropertyName("Shop Category")]
        public string ShopCategory { get; set; }= string.Empty;
        [JsonPropertyName("storeAddress")]
        public string StoreAddress { get; set; }   = string.Empty;
    }

}

