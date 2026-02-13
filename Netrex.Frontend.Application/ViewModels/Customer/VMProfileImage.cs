namespace Netrex.Frontend.Application.ViewModels.Customer
{ 
    public class VMProfileImage  
    {
        public Guid PublicId { get; set; }
        public Guid UserId { get; set; }
        public byte[] ImageData { get; set; } = [];
    }
}
