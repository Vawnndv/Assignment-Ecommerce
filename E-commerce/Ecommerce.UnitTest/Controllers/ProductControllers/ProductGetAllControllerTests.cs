using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Controllers;
using Backend.Interfaces;
using Shared_ViewModels.Helpers;
using Shared_ViewModels.Product;
using System.Collections.Generic;
using Backend.UnitOfWork.Product;
using Backend.Models;
using AutoFixture;
using System.Linq;
using Backend.Mappers;

namespace Ecommerce.UnitTest.Controllers.ProductControllers
{
    public class ProductGetAllControllerTests
    {
        private readonly Fixture _fixture;

        public ProductGetAllControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAll_ReturnsOkObjectResult_WhenProductsExist()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var mockQueryObject = _fixture.Create<QueryObject>();
            var mockProducts = _fixture.CreateMany<Product>(2).ToList(); // Create random 2 product
            var mockProductDtos = mockProducts.Select(p => p.ToProductDto()).ToList(); // Create random 2 ProductDto

            mockUnitOfWork.Setup(u => u.ProductRepository.GetAllAsync(mockQueryObject)).ReturnsAsync(mockProducts);

            var controller = new ProductController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetAll(mockQueryObject);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsAssignableFrom<IEnumerable<ProductVmDto>>(okResult.Value);
            Assert.Equal(mockProducts.Count(), returnedProducts.Count());
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList_WhenNoProductsExist()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var mockQueryObject = _fixture.Create<QueryObject>();
            var mockProducts = new List<Product>();
            var mockProductDtos = new List<ProductVmDto>();

            mockUnitOfWork.Setup(u => u.ProductRepository.GetAllAsync(mockQueryObject)).ReturnsAsync(mockProducts);

            var controller = new ProductController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetAll(mockQueryObject);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsAssignableFrom<IEnumerable<ProductVmDto>>(okResult.Value);
            Assert.Empty(returnedProducts);
        }

        [Fact]
        public async Task GetAll_ReturnsBadRequestObjectResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var mockQueryObject = _fixture.Create<QueryObject>();
            var controller = new ProductController(mockUnitOfWork.Object);
            controller.ModelState.AddModelError("Key", "Error Message");

            // Act
            var result = await controller.GetAll(mockQueryObject);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
