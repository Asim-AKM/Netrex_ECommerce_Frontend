using System.Globalization;

namespace Netrex.Frontend.Application.ViewModels.Customer
{
    public class VMCustomer
    {
        public Guid UserId { get; set; } // Ye zaroori hai update ke liye
        public string Name { get; set; } = "";
        public string City { get; set; } = "";
        public string Province { get; set; } = "";
        public string Country { get; set; } = "";
        public string Address { get; set; } = "";
        public ProfileImage Images {  get; set; }= new ProfileImage();
    }
    public class ProfileImage()
    {
        public string ImageURL { get; set; }=string.Empty;
        public string CloudPublicId { get; set; }= string.Empty;
    }
}
