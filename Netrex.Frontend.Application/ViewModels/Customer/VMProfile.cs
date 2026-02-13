namespace Netrex.Frontend.Application.ViewModels.Customer
{
    public class VMProfile
    {
        public Guid UserId { get; set; } // Ye zaroori hai update ke liye
        public string Name { get; set; } = "";
        public string City { get; set; } = "";
        public string Province { get; set; } = "";
        public string Country { get; set; } = "";
        public string Address { get; set; } = "";
    }
}
