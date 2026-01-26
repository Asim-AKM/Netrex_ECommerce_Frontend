using Netrex.Frontend.Application.Commons.Enums;
using Netrex.Frontend.Application.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.Services.Common
{
    public class ToastService
    {
        private readonly object _lockObject = new object();
        public event Action<ToastMessage>? OnShow;

        /// <summary>
        /// Creates a new ToastBuilder for fluent API chaining.
        /// Example: _toast.Notify().WithTitle("Success").WithType(ToastType.Success).Show();
        /// </summary>
        public ToastBuilder Notify()
        {
            return new ToastBuilder(this);
        }

        /// <summary>
        /// Internal method called by ToastBuilder to show the toast.
        /// </summary>
        internal void ShowToast(ToastMessage toast)
        {
            if (toast == null)
                throw new ArgumentNullException(nameof(toast));

            lock (_lockObject)
            {
                OnShow?.Invoke(toast);
            }
        }

        /// <summary>
        /// Shows a toast notification with explicit parameters.
        /// </summary>
        public void Show(string title, string message, ToastType type, int duration = 4000)
        {
            var toast = new ToastMessage
            {
                Title = title,
                Message = message,
                Type = type,
                Duration = duration
            };

            ShowToast(toast);
        }

        // Helper methods for cleaner calling (backward compatible)
        public void Success(string message, string title = "Success") => Show(title, message, ToastType.Success);
        public void Error(string message, string title = "Error") => Show(title, message, ToastType.Error);
        public void Info(string message, string title = "Info") => Show(title, message, ToastType.Info);
        public void Warning(string message, string title = "Warning") => Show(title, message, ToastType.Warning);
    }

}

