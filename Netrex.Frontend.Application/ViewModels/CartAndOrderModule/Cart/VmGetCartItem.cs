namespace Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart
{
    public  class VmGetCartItem
    {
        public Guid CartItemId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}
