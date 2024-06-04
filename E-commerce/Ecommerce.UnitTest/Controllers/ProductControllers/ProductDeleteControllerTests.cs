using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Controllers;
using Backend.Interfaces;
using Backend.UnitOfWork.Product;
using Backend.Models;
using AutoFixture;

namespace Ecommerce.UnitTest.Controllers.ProductControllers
{
    public class ProductDeleteControllerTests
    {
        private readonly Fixture _fixture;

        public ProductDeleteControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenProductIsDeleted()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var productId = 1;
            var mockProduct = _fixture.Create<Product>();

            mockUnitOfWork.Setup(u => u.ProductRepository.DeleteAsync(productId)).ReturnsAsync(mockProduct);
            var controller = new ProductController(mockUnitOfWork.Object);

            // Act
            var result = await controller.Delete(productId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var productId = 1;

            mockUnitOfWork.Setup(u => u.ProductRepository.DeleteAsync(productId)).ReturnsAsync((Product)null);
            var controller = new ProductController(mockUnitOfWork.Object);

            // Act
            var result = await controller.Delete(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest_WithInvalidModel()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var controller = new ProductController(mockUnitOfWork.Object);
            controller.ModelState.AddModelError("key", "error message");

            // Act
            var result = await controller.Delete(1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}