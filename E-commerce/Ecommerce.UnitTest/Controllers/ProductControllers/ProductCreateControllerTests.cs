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
    public class ProductCreateControllerTests
    {
        private readonly Fixture _fixture;

        public ProductCreateControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WithValidModel()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var mockProductDto = _fixture.Create<CreateProductRequestVmDto>();
            var mockProduct = _fixture.Create<Product>();

            mockUnitOfWork.Setup(u => u.ProductRepository.CreateAsync(It.IsAny<Product>())).ReturnsAsync(mockProduct);
            var controller = new ProductController(mockUnitOfWork.Object);

            // Act
            var result = await controller.Create(mockProductDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(ProductController.GetById), createdAtActionResult.ActionName);
            Assert.IsType<ProductVmDto>(createdAtActionResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WithInvalidModel()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IProductUnitOfWork>();
            var controller = new ProductController(mockUnitOfWork.Object);
            controller.ModelState.AddModelError("key", "error message");

            // Act
            var result = await controller.Create(new CreateProductRequestVmDto());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
