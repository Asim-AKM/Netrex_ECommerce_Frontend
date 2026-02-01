using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.ProductManagement;

namespace Netrex.Frontend.Application.Services.ProductManagement.Interfaces
{
    public interface ICloudnaryManager
    {
        Task<ApiResponse<CloudinaryUploadResult>> UploadImageToCloudinary(byte[] imageBytes, string fileName, string contentType);
    }
}
