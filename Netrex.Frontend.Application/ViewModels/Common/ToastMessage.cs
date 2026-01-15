using Netrex.Frontend.Application.Commons.Enums;

namespace Netrex.Frontend.Application.ViewModels.Common
{
    public class ToastMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public string? Message { get; set; }
        public ToastType Type { get; set; } = ToastType.Info;
        public int Duration { get; set; } // Duration in milliseconds
    }
}
