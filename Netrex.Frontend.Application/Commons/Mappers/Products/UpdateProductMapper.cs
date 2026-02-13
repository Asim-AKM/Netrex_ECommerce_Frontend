using Netrex.Frontend.Application.DTO_s.ProductDto;
using Netrex.Frontend.Application.ViewModels.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.Commons.Mappers.Products
{
    public static class UpdateProductMapper
    {
        public static UpdateProductDto MapToUpdateDto(this ProductsVm productsVm)
        {
            return new UpdateProductDto
            {
                ProductId = productsVm.ProductId,
                ProductName = productsVm.ProductName,
                ProductDescription = productsVm.ProductDescription,
                CategoryName = productsVm.CategoryName,
                Price = productsVm.Price,
                Discount = productsVm.Discount,
                StockQuantity = productsVm.StockQuantity,
                CreatedAt = productsVm.CreatedAt,
                ImageUrl = productsVm.ImageUrl,
                CloudPublicId = productsVm.CloudPublicId,
                IsPrimary = productsVm.IsPrimary,
                UploadedAt = productsVm.UploadedAt
            };
        }
    }
}
