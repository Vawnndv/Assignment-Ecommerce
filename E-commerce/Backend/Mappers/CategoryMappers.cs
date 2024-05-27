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

        public static List<CategoryVmDto> ToListCategoryDto(this List<Category> categoryModel)
        {
            if (categoryModel == null)
            {
                return new List<CategoryVmDto>();
            }

            return categoryModel.Select(category => new CategoryVmDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ParentCategoryId = category.ParentCategoryId,
            }).ToList();
        }
    }
}
