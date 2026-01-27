using Netrex.Frontend.Application.Commons.Enums;

namespace Netrex.Frontend.Application.ViewModels.Common
{
    /// <summary>
    /// Represents a toast notification message with support for custom icons and sounds.
    /// </summary>
    public class ToastMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public string? Message { get; set; }
        public ToastType Type { get; set; } = ToastType.Info;
        public int Duration { get; set; } // Duration in milliseconds
        public string? IconClass { get; set; } // Custom Bootstrap Icons class (e.g., "bi bi-cart", "bi bi-credit-card")
        public bool PlaySound { get; set; } = true; // Whether to play sound for this toast (default: true)
        public double SoundVolume { get; set; } = 0.5; // Sound volume (0.0 to 1.0, default: 0.5)
    }
}
