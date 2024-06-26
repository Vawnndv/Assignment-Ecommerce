﻿using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Shared_ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class ProductRatingRepository : IProductRatingRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductRatingRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ProductRating> CreateAsync(ProductRating productRatingModel)
        {
            await _context.ProductRatings.AddAsync(productRatingModel);
            return productRatingModel;
        }

        public async Task<ProductRating?> DeleteAsync(int id)
        {
            var productRatingModel = await _context.ProductRatings.FirstOrDefaultAsync(x => x.Id == id);

            if (productRatingModel == null)
            {
                return null;
            }
            _context.ProductRatings.Remove(productRatingModel);
            return productRatingModel;
        }

        public async Task<List<ProductRating>> GetAllByProductIdAsync(int id)
        {
            return await _context.ProductRatings.Where(c => c.ProductId == id).ToListAsync();
        }

        public async Task<ProductRating?> GetByIdAsync(int id)
        {
            return await _context.ProductRatings.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> ProductRatingExists(int id)
        {
            return await _context.ProductRatings.AnyAsync(s => s.Id == id);
        }

        public async Task<ProductRating?> UpdateAsync(string userId, int id, UpdateProductRatingVmDto productRatingDto)
        {
            var existingProductRating = await _context.ProductRatings.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProductRating == null)
            {
                return null;
            }

            existingProductRating.Rating = productRatingDto.Rating;
            existingProductRating.Review = productRatingDto.Review;
            existingProductRating.AppUserId = userId;

            return existingProductRating;
        }
    }
}
