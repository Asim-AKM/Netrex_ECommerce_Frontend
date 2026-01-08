using System.Text.Json.Serialization;

namespace Netrex.Frontend.Application.ViewModels.UserManagement.Authentication
{
    public class VmRegister
    {
        [JsonPropertyName("fullName")]
        public string? FullName { get; set; }
        [JsonPropertyName("username")]
        public string? UserName { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }

    }
}
