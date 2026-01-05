using Netrex.Frontend.Blazor.Models;

namespace Netrex.Frontend.Blazor.Services
{
    public interface IAuthService
    {
        Task<bool> UserRegisterAsync(VMUserRegistration obj);
    }
}
