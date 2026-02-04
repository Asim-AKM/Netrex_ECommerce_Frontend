namespace Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart
{
    public class CartItemState
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }=string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public double Price { get; set; }

        public int Quantity { get; set; }

        public double SubTotal => Price * Quantity;
    }
}
