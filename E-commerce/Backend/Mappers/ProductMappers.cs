using Backend.Models;
using Shared_ViewModels.Category;
using Shared_ViewModels.Product;

namespace Backend.Mappers
{
    public static class ProductMappers
    {
        public static ProductImageVmDto ToProductImageDto(this ProductImage productModel)
        {
            return new ProductImageVmDto
            {
                Id = productModel.Id,
                ImageUrl = productModel.ImageUrl,
                ProductTypeId = productModel.ProductTypeId
            };
        }

        public static ProductTypeVmDto ToProductTypeDto(this ProductType productModel)
        {
            return new ProductTypeVmDto
            {
                Id = productModel.Id,
                Type = productModel.Type,
                Description = productModel.Description,
                ProductId = productModel.ProductId,
                ProductImages = productModel.ProductImages.Select(s => s.ToProductImageDto()).ToList()
            };
        }

        public static ProductVmDto ToProductDto(this Product productModel)
        {
            return new ProductVmDto
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                Discount = productModel.Discount,
                CreatedDate = productModel.CreatedDate,
                UpdatedDate = productModel.UpdatedDate,
                CategoryId = productModel.CategoryId,
                ProductTypes = productModel.ProductTypes.Select(s => s.ToProductTypeDto()).ToList()
            };
        }

        public static ProductImage ToProductImageFromCreateDTO(this CreateProductImageRequestVmDto productDto)
        {
            return new ProductImage
            {
                ImageUrl = productDto.ImageUrl
            };
        }

        public static ProductType ToProductTypeFromCreateDTO(this CreateProductTypeRequestVmDto productDto)
        {
            return new ProductType
            {
                Type = productDto.Type,
                Description = productDto.Description,
                ProductImages = productDto.ProductImages.Select(s => s.ToProductImageFromCreateDTO()).ToList()
            };
        }

        public static Product ToProductFromCreateDTO(this CreateProductRequestVmDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Discount = productDto.Discount,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CategoryId = productDto.CategoryId,
                ProductTypes = productDto.ProductTypes.Select(s => s.ToProductTypeFromCreateDTO()).ToList()
            };
        }
    }
}
