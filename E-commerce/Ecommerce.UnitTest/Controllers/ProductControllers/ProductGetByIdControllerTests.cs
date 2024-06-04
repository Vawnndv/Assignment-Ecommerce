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
    public class ProductGetByIdControllerTests
    {
        private readonly Fixture _fixture;

        public ProductGetByIdControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetById_ReturnsOkObjectResult_WhenProductExists()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var productId = 1;
            var mockProduct = _fixture.Build<Product>()
                .With(p => p.Id, productId)
                .Create();
            var mockProductDto = mockProduct.ToProductDto();

            mockUnitOfWork.Setup(u => u.ProductRepository.GetByIdAsync(productId)).ReturnsAsync(mockProduct);

            var controller = new ProductController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetById(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<ProductVmDto>(okResult.Value);
            Assert.Equal(mockProductDto.Id, returnedProduct.Id);
            // Add more assertions if needed for other properties
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var productId = 1;

            mockUnitOfWork.Setup(u => u.ProductRepository.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            var controller = new ProductController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetById(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_ReturnsBadRequestObjectResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var productId = 1;
            var controller = new ProductController(mockUnitOfWork.Object);
            controller.ModelState.AddModelError("Key", "Error Message");

            // Act
            var result = await controller.GetById(productId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
