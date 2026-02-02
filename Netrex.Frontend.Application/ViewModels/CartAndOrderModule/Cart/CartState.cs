namespace Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart
{
    public class CartState
    {
        public List<CartItemState> Items { get; set; } = new();

        public double TotalAmount => Items.Sum(x => x.SubTotal);

        public int TotalItems => Items.Sum(x => x.Quantity);
    }
}
