namespace Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Order
{
    public class VmGetOrder
    {
        public Guid OrderId { get; set; }
        public bool OrderStatus { get; set; }
        public double TotalAmount {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
