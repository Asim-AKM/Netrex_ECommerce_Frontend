using Netrex.Frontend.Application.Commons.Enums;
using Netrex.Frontend.Application.ViewModels.Common;

namespace Netrex.Frontend.Application.Services.Common
{
    /// <summary>
    /// Fluent API builder for creating toast notifications.
    /// Enables chainable method calls for better developer experience.
    /// </summary>
    public class ToastBuilder
    {
        private readonly ToastService _toastService;
        private readonly ToastMessage _toastMessage;

        internal ToastBuilder(ToastService toastService)
        {
            _toastService = toastService;
            _toastMessage = new ToastMessage
            {
                Duration = 4000, // Default duration
                Type = ToastType.Info // Default type
            };
        }

        /// <summary>
        /// Sets the toast title.
        /// </summary>
        public ToastBuilder WithTitle(string title)
        {
            _toastMessage.Title = title;
            return this;
        }

        /// <summary>
        /// Sets the toast message/body.
        /// </summary>
        public ToastBuilder WithMessage(string message)
        {
            _toastMessage.Message = message;
            return this;
        }

        /// <summary>
        /// Sets the toast type (Success, Error, Info, Warning).
        /// </summary>
        public ToastBuilder WithType(ToastType type)
        {
            _toastMessage.Type = type;
            return this;
        }

        /// <summary>
        /// Sets the duration in milliseconds before auto-dismiss.
        /// </summary>
        public ToastBuilder WithDuration(int milliseconds)
        {
            _toastMessage.Duration = milliseconds;
            return this;
        }

        /// <summary>
        /// Sets a custom Bootstrap Icons class for the toast icon.
        /// Example: WithIcon("bi bi-cart") or WithIcon("bi bi-credit-card")
        /// </summary>
        public ToastBuilder WithIcon(string iconClass)
        {
            _toastMessage.IconClass = iconClass;
            return this;
        }

        /// <summary>
        /// Enables or disables sound for this toast notification.
        /// Default is true (sound enabled).
        /// </summary>
        public ToastBuilder WithSound(bool playSound = true)
        {
            _toastMessage.PlaySound = playSound;
            return this;
        }

        /// <summary>
        /// Sets the sound volume for this toast notification.
        /// Value should be between 0.0 (silent) and 1.0 (full volume).
        /// Default is 0.5 (50% volume).
        /// </summary>
        public ToastBuilder WithSoundVolume(double volume)
        {
            _toastMessage.SoundVolume = Math.Clamp(volume, 0.0, 1.0);
            return this;
        }

        /// <summary>
        /// Displays the toast notification.
        /// </summary>
        public void Show()
        {
            _toastService.ShowToast(_toastMessage);
        }
    }
}
