using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.ViewModels.UserManagement.Authentication
{
    public class VmVerifyOtp
    {

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("otp")]
        public string Otp { get; set; } = string.Empty;

    }
}
