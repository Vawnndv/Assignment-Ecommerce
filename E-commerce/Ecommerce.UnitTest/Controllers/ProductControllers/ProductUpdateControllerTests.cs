using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Controllers;
using Backend.Interfaces;
using Shared_ViewModels.Product;
using Backend.UnitOfWork.Product;
using Backend.Models;
using AutoFixture;

namespace Ecommerce.UnitTest.Controllers.ProductControllers
{
    public class ProductUpdateControllerTests
    {
        private readonly Fixture _fixture;

        public ProductUpdateControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task Update_ReturnsOkObjectResult_WithValidModel()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var productId = 1;
            var mockUpdateDto = _fixture.Create<UpdateProductVmDto>();
            var mockProduct = _fixture.Create<Product>();

            mockUnitOfWork.Setup(u => u.ProductRepository.UpdateAsync(productId, mockUpdateDto)).ReturnsAsync(mockProduct);
            var controller = new ProductController(mockUnitOfWork.Object);

            // Act
            var result = await controller.Update(productId, mockUpdateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var productDto = Assert.IsType<ProductVmDto>(okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var productId = 1;
            var mockUpdateDto = _fixture.Create<UpdateProductVmDto>();

            mockUnitOfWork.Setup(u => u.ProductRepository.UpdateAsync(productId, mockUpdateDto)).ReturnsAsync((Product)null);
            var controller = new ProductController(mockUnitOfWork.Object);

            // Act
            var result = await controller.Update(productId, mockUpdateDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WithInvalidModel()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var controller = new ProductController(mockUnitOfWork.Object);
            controller.ModelState.AddModelError("key", "error message");

            // Act
            var result = await controller.Update(1, new UpdateProductVmDto());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
