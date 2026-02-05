using Netrex.Frontend.Application.DTO_s;

namespace Netrex.Frontend.Application.Services.Customer.Interfaces
{
    public interface ICustomerManager
    {
        Task<bool> UpdateCustomerAsync(UpdateCustomerDto customer);
    }
}
