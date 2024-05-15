using Backend.Models;
using Shared_ViewModels.Category;

namespace Backend.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryVmDto ToCategoryDto(this Category categoryModel)
        {
            return new CategoryVmDto
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name,
                Description = categoryModel.Description,
                ParentCategoryId = categoryModel.ParentCategoryId,
                SubCategories = categoryModel.SubCategories.Select(s => s.ToCategoryDto()).ToList()
            };
        }

        public static Category ToCategoryFromCreateDTO(this CreateCategoryRequestVmDto categoryDto)
        {
            return new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                ParentCategoryId = categoryDto.ParentCategoryId
            };
        }
    }
}
