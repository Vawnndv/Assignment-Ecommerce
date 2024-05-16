using Backend.Data;
using Backend.Helpers;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Shared_ViewModels.Product;

namespace Backend.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product productModel)
        {
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var productModel = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (productModel == null)
            {
                return null;
            }
            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<List<Product>> GetAllAsync(QueryObject query)
        {
            var products = _context.Products
                .Include(p => p.ProductTypes)
                    .ThenInclude(pt => pt.ProductImages)
                .Include(p => p.Ratings)
                .AsQueryable();

            if (query.IsLatest)
            {
                products = products.OrderByDescending(p => p.CreatedDate).Take(query.PageLimit);
            }

            if (query.IsDiscount)
            {
                products = products.OrderByDescending(p => p.Discount).Take(query.PageLimit);
            }

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                products = products.Where(s => s.Name.Contains(query.Search));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDecsending ? products.OrderByDescending(s => s.Name) : products.OrderBy(s => s.Name);
                }

                else if (query.SortBy.Equals("CreatedDate", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDecsending ? products.OrderByDescending(s => s.CreatedDate) : products.OrderBy(s => s.CreatedDate);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await products.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        private List<int> GetAllCategoryIds(Category category)
        {
            var categoryIds = new List<int> { category.Id };

            if (category.SubCategories != null && category.SubCategories.Any())
            {
                foreach (var subCategory in category.SubCategories)
                {
                    categoryIds.AddRange(GetAllCategoryIds(subCategory));
                }
            }

            return categoryIds;
        }

        public async Task<List<Product>?> GetByCategoryAsync(int id, QueryObject query)
        {
            // Find the category by categoryId
            var category = _context.Categories
                .Include(s => s.SubCategories)
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            var categoryIds = GetAllCategoryIds(category);

            var products = _context.Products
                .Where(p => categoryIds.Contains(p.CategoryId))
                .Include(p => p.ProductTypes)
                    .ThenInclude(pt => pt.ProductImages)
                .Include(p => p.Ratings)
                .AsQueryable();

            // Query
            if (query.IsLatest)
            {
                products = products.OrderByDescending(p => p.CreatedDate).Take(query.PageLimit);
            }

            if (query.IsDiscount)
            {
                products = products.OrderByDescending(p => p.Discount).Take(query.PageLimit);
            }

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                products = products.Where(s => s.Name.Contains(query.Search));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDecsending ? products.OrderByDescending(s => s.Name) : products.OrderBy(s => s.Name);
                }

                else if (query.SortBy.Equals("CreatedDate", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDecsending ? products.OrderByDescending(s => s.CreatedDate) : products.OrderBy(s => s.CreatedDate);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await products.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductTypes)
                    .ThenInclude(pt => pt.ProductImages)
                .Include(p => p.Ratings)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<bool> ProductExists(int id)
        {
            return _context.Products.AnyAsync(s => s.Id == id);
        }

        public async Task<Product?> UpdateAsync(int id, UpdateProductVmDto productDto)
        {
            var existingProduct = await _context.Products
                .Include(p => p.ProductTypes)
                    .ThenInclude(pt => pt.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.Discount = productDto.Discount;
            existingProduct.UpdatedDate = DateTime.Now;

            // Update or add ProductTypes
            foreach (var productTypeDto in productDto.ProductTypes)
            {
                var existingProductType = existingProduct.ProductTypes.FirstOrDefault(pt => pt.Id == productTypeDto.Id);
                if (existingProductType != null)
                {
                    // Update existing ProductType properties
                    existingProductType.Type = productTypeDto.Type;
                    existingProductType.Description = productTypeDto.Description;
                }
                else
                {
                    // Create a new ProductType
                    var newProductType = new ProductType
                    {
                        Type = productTypeDto.Type,
                        Description = productTypeDto.Description,
                        ProductId = existingProduct.Id // Set the ProductId
                    };
                    existingProduct.ProductTypes.Add(newProductType);
                }

                // Update or add ProductImages for the ProductType
                foreach (var productImageDto in productTypeDto.ProductImages)
                {
                    var existingProductImage = existingProductType.ProductImages.FirstOrDefault(pi => pi.Id == productImageDto.Id);
                    if (existingProductImage != null)
                    {
                        // Update existing ProductImage properties
                        existingProductImage.ImageUrl = productImageDto.ImageUrl;
                    }
                    else
                    {
                        // Create a new ProductImage
                        var newProductImage = new ProductImage
                        {
                            ImageUrl = productImageDto.ImageUrl,
                            ProductTypeId = existingProductType.Id // Set the ProductTypeId
                        };
                        existingProductType.ProductImages.Add(newProductImage);
                    }
                }
            }

            // Remove ProductTypes not present in the DTO
            foreach (var existingProductType in existingProduct.ProductTypes.ToList())
            {
                if (!productDto.ProductTypes.Any(dto => dto.Id == existingProductType.Id))
                {
                    // Remove the ProductType
                    existingProduct.ProductTypes.Remove(existingProductType);
                }
                 // Remove ProductImages not present in the DTO for all ProductTypes
                 foreach (var existingProductImage in existingProductType.ProductImages)
                 {
                    var productTypeDto = productDto.ProductTypes.FirstOrDefault(pi => pi.Id == existingProductType.Id);
                    if (productTypeDto != null && !productTypeDto.ProductImages.Any(dto => dto.Id == existingProductImage.Id))
                    {
                        // Remove the ProductImages
                        existingProductType.ProductImages.Remove(existingProductImage);
                    }
                 }
            }

            await _context.SaveChangesAsync();
    
            return existingProduct;
        }
    }
}
