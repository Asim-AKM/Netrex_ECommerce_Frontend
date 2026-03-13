using Netrex.Frontend.Application.DTO_s.ProductDto;
using Netrex.Frontend.Application.ViewModels.ProductManagement;

namespace Netrex.Frontend.Application.Commons.Mappers.Products
{
    public static class AddProductMapper
    {
        public static AddProductDto Map(this ProductsVm vm)
        {
            return new AddProductDto
            {
                SellerId=vm.SellerId,
                ProductName = vm.ProductName,
                ProductDescription = vm.ProductDescription ?? string.Empty,
                CategoryName = vm.CategoryName,
                Price = (double)vm.Price,
                Discount = (double)vm.Discount,
                StockQuantity = vm.StockQuantity,

                
                Images = vm.Images?.Select(img => new ImageDto
                {
                    ImageUrl = img.Url!,
                    CloudPublicId = img.CloudPublicId!,
                    IsPrimary = img.IsPrimary
                }).ToList() ?? new List<ImageDto>()
            };
        }
    }
}
