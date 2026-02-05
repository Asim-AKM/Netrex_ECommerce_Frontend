namespace Netrex.Frontend.Application.DTO_s
{
    public class UpdateCustomerDto
    {
        public Guid UserId { get; set; } // Ye zaroori hai update ke liye
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string City { get; set; } = "";
        public string Province { get; set; } = "";
        public string Country { get; set; } = "";
        public string Address { get; set; } = "";
    }
}