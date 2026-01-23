using Netrex.Frontend.Application.Services.SellerAndShop.Interfaces;
using Netrex.Frontend.Application.ViewModels.SellerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.Services.SellerAndShop.Implementations
{
    public class SellerAndShop : ISellerAndshop
    {
        public Task<string> CreateSellerAsync(VmSeller vmSeller)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteSellerAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<VmSeller> GetBYIDSellerAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<VmSeller>> GetSellerAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateSellerAsync(VmSeller vmSeller)
        {
            throw new NotImplementedException();
        }
    }
}
