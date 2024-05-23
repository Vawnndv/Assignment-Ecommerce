using Shared_ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Helpers
{
    public static class RatingHelper
    {
        public static int CalculateAverageRating(ICollection<ProductRatingVmDto> ratings)
        {
            if (ratings == null || !ratings.Any())
            {
                return 0; // No ratings
            }

            // Calculate the average rating and round it to the nearest integer
            var averageRating = ratings.Average(r => r.Rating);
            var roundedRating = (int)Math.Round(averageRating, MidpointRounding.AwayFromZero);

            // Ensure the rating is within the range of 1 to 5
            return Math.Clamp(roundedRating, 1, 5);
        }
    }
}
