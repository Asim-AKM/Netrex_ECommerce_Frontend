using System.Text.Json.Serialization;

namespace Netrex.Frontend.Application.ViewModels.UserManagement.Authentication
{
    public class VmLogin
    {
        [JsonPropertyName("userIdentifier")]
        public string? Email { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
