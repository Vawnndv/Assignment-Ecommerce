using Backend.Models;
using Shared_ViewModels.Category;
using Shared_ViewModels.Product;

namespace Backend.Mappers
{
    public static class ProductRatingMappers
    {
        public static ProductRatingVmDto ToProductRatingDto(this ProductRating productRatingModel)
        {
            return new ProductRatingVmDto
            {
                Id = productRatingModel.Id,
                Rating = productRatingModel.Rating,
                Review = productRatingModel.Review,
                ProductId = productRatingModel.ProductId,
            };
        }

        public static ProductRating ToProductRatingFromCreateDTO(this CreateProductRatingRequestVmDto productRatingDto, AppUser appUser)
        {
            return new ProductRating
            {
                Rating = productRatingDto.Rating,
                Review = productRatingDto.Review,
                ProductId = productRatingDto.ProductId,
                AppUserId = appUser.Id,
            };
        }
    }
}
