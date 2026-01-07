using System.Text.Json.Serialization;

namespace Netrex.Frontend.Application.ViewModels.UserManagement.Authentication
{
    public class RegisterViewModel
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
        [JsonPropertyName("contact")]
        public string? Contact { get; set; }
        [JsonPropertyName("username")]
        public string? UserName { get; set; }
    }
}
