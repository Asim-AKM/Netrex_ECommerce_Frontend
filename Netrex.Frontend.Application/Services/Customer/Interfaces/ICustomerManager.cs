using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.Customer;

namespace Netrex.Frontend.Application.Services.Customer.Interfaces
{
    public interface ICustomerManager
    {
        public Task<ApiResponse<string>> UpdateCustomer(VMCustomer customer, byte[]? newImageBytes = null,string? newImageName = null);
        public Task<ApiResponse<string>> DeleteCustomer(Guid customerId);
        public Task<ApiResponse<List<VMCustomer>>> GetAllCustomers();

        
    }
}
