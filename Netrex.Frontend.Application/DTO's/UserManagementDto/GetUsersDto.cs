using Domain_Service.Enums;

namespace Netrex.Frontend.Blazor.DTOs
{

    public class GetUsersDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = "";
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public string Contact { get; set; } = "";
        public UserStatus Userstatus { get; set; }
        public DateOnly CreateAt { get; set; }
        public RoleType RoleName { get; set; }
    }
}