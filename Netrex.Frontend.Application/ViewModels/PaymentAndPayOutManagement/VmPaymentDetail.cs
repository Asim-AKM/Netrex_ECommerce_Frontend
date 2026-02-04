using Netrex.Frontend.Application.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.ViewModels.PaymentAndPayOutManagement
{
    public class VmPaymentDetail
    {
        [JsonPropertyName("PaymentDetailId")]
        public Guid PaymentDetailId { get; set; }

        [JsonPropertyName("OrderId")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("PaymentMethod")]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonPropertyName("TransactionId")]
        public string TransactionId { get; set; } = string.Empty;

        [JsonPropertyName("PaymentStatus")]
        public PaymentStatus PaymentStatus { get; set; }

        [JsonPropertyName("AmountPaid")]
        public double AmountPaid { get; set; }

        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
