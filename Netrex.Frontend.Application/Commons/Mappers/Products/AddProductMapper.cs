using Netrex.Frontend.Application.DTO_s.ProductDto;
using Netrex.Frontend.Application.ViewModels.ProductManagement;

namespace Netrex.Frontend.Application.Commons.Mappers.Products
{
    public static class AddProductMapper
    {
        public static AddProductDto Map(this ProductsVm vm)
        {
            return new AddProductDto
            (
                vm.ProductName,
                vm.ProductDescription,
                vm.CategoryName,
                vm.Price,
                vm.Discount,
                vm.StockQuantity,
                vm.CreatedAt,
                vm.ImageUrl,
                vm.CloudPublicId,
                vm.IsPrimary,
                vm.UploadedAt
            );
        }
    }
}
