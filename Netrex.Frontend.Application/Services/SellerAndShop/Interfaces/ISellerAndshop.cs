using Netrex.Frontend.Application.ViewModels.SellerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.Services.SellerAndShop.Interfaces
{
    public interface ISellerAndshop
    {
       
        Task<string> CreateSellerAsync(VmSeller vmSeller);
        Task<string> UpdateSellerAsync(VmSeller vmSeller);
        Task<string> DeleteSellerAsync(Guid Id);
        Task<List<VmSeller>> GetSellerAsync(); 
        Task<VmSeller>   GetBYIDSellerAsync(Guid Id);

    }
}
