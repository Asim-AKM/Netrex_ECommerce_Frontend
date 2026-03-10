namespace Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Order
{
    public class VmOrderSummary
    {
        public string ProductName { get; set; }=string.Empty;
        public int Quantity { get; set; }
        public double price { get; set; }
        public double total { get; set; }
    }
}
