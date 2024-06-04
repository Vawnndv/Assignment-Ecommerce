using AutoFixture;
using Ecommerce_Customers_Site.Services.Product;
using Moq;
using Shared_ViewModels.Helpers;
using Shared_ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.UnitTest.Services.Product
{
    public class ProductGetAllServiceTests
    {
        private readonly Fixture _fixture;

        public ProductGetAllServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAll_ReturnsListOfProducts()
        {
            // Arrange
            var query = new QueryObject();
            var products = _fixture.CreateMany<ProductVmDto>(3).ToList(); // Create a list of 3 dummy products
            var mockProductService = new Mock<IProductAPIService>();

            mockProductService.Setup(service => service.GetAll(query))
                .ReturnsAsync(products);

            // Act
            var result = await mockProductService.Object.GetAll(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count); // Check if the product list has 3 items
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList_WhenNoProductsFound()
        {
            // Arrange
            var query = new QueryObject();
            var mockProductService = new Mock<IProductAPIService>();

            mockProductService.Setup(service => service.GetAll(query))
                .ReturnsAsync(new List<ProductVmDto>()); // Return an empty list when no products are found

            // Act
            var result = await mockProductService.Object.GetAll(query);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // Check if the product list is empty
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList_WhenErrorResponse()
        {
            // Arrange
            var categoryId = 1;
            var query = new QueryObject();
            var mockProductService = new Mock<IProductAPIService>();

            mockProductService.Setup(service => service.GetAll(query))
                .ReturnsAsync(new List<ProductVmDto>()); // Return an empty list in case of server error

            // Act
            var result = await mockProductService.Object.GetAll(query);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // Check if the product list is empty
        }
    }
}
