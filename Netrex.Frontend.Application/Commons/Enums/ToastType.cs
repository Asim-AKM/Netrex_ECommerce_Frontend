using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.Commons.Enums
{
    /// <summary>
    /// Toast notification types for e-commerce application.
    /// Includes standard types and e-commerce specific types.
    /// </summary>
    public enum ToastType
    {
        // Standard Types
        Success,
        Error,
        Info,
        Warning,

        // E-Commerce Specific Types
        Cart,           // Shopping cart operations
        Payment,        // Payment related notifications
        Order,          // Order processing notifications
        Shipping,       // Shipping and delivery notifications
        Wishlist,       // Wishlist operations
        Review,         // Product review notifications
        Discount,       // Discount and coupon notifications
        Stock,          // Stock and inventory notifications
        Notification    // General notifications
    }
}
