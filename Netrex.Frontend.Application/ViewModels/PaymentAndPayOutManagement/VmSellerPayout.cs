using Netrex.Frontend.Application.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.ViewModels.PaymentAndPayOutManagement
{
    public class VmSellerPayout
    {
        [JsonPropertyName("SellerPayoutId")]
        public Guid SellerPayoutId { get; set; }

        [JsonPropertyName("SellerId")]
        public Guid SellerId { get; set; }

        [JsonPropertyName("OrderId")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("AmountToPay")]
        public double AmountToPay { get; set; }

        [JsonPropertyName("PaymentStatus")]
        public PaymentStatus PaymentStatus { get; set; }

        [JsonPropertyName("PayOutDate")]
        public DateTime PayOutDate { get; set; }
    }
}
