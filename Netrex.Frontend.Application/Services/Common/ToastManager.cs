using Netrex.Frontend.Application.Commons.Enums;
using Netrex.Frontend.Application.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.Services.Common
{
    public class ToastManager
    {
        public event Action<ToastMessage>? OnShow;

        public void Show(string title, string message, ToastType type, int duration = 4000)
        {
            var toast = new ToastMessage
            {
                Title = title,
                Message = message,
                Type = type,
                Duration = duration
            };

            OnShow?.Invoke(toast);
        }

        // Helper methods for cleaner calling
        public void Success(string message, string title = "Success") => Show(title, message, ToastType.Success);
        public void Error(string message, string title = "Error") => Show(title, message, ToastType.Error);
        public void Info(string message, string title = "Info") => Show(title, message, ToastType.Info);
        public void Warning(string message, string title = "Warning") => Show(title, message, ToastType.Warning);

    }
}
