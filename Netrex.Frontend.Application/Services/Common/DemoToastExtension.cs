using Netrex.Frontend.Application.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.Services.Common
{
    public static class DemoToastExtension
    {
        public static void CardUpdated(this ToastService toastService,string message)
        {
            toastService.Notify()
                        .WithTitle("Cart Updated")
                        .WithMessage(message)
                        .WithType(ToastType.Cart);
        }
    }
}
