using System.Text.Json.Serialization;

namespace Netrex.Frontend.Blazor.Models
{
    public class VMUserRegistration
    {

        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("contact")]
        public string Contact  { get; set; }
        [JsonPropertyName("username")]
        public string UserName { get; set; }

    }
}
