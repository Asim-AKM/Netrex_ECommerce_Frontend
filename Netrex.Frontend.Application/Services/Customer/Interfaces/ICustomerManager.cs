using Netrex.Frontend.Application.DTO_s;

namespace Netrex.Frontend.Application.Services.Customer.Interfaces
{
    public interface ICustomerManager
    {
        Task UpdateCustomerAsync(UpdateCustomerDto customer);
        Task UpdateProfileImageAsync(Guid userId, byte[] imageData);
    }
}
