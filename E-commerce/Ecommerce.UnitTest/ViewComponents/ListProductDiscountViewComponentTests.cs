using Ecommerce_Customers_Site.Components.Product;
using Ecommerce_Customers_Site.Services.Product;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Shared_ViewModels.Product;
using Shared_ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;

namespace Ecommerce.UnitTest.ViewComponents
{
    public class ListProductDiscountViewComponentTests
    {
        private readonly Fixture _fixture;

        public ListProductDiscountViewComponentTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task InvokeAsync_ReturnsViewWithProducts()
        {
            // Arrange
            var mockProductService = new Mock<IProductAPIService>();
            var products = _fixture.CreateMany<ProductVmDto>(2).ToList();

            mockProductService.Setup(service => service.GetAll(It.Is<QueryObject>(q => q.IsDiscount == false)))
                .ReturnsAsync(products);

            var component = new ListProductNewViewComponent(mockProductService.Object);

            // Act
            var result = await component.InvokeAsync() as ViewViewComponentResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<IList<ProductVmDto>>(result.ViewData.Model);
            Assert.Equal(2, model.Count);
            Assert.Equal(products[0].Name, model[0].Name);
            Assert.Equal(products[1].Name, model[1].Name);
        }

        [Fact]
        public async Task InvokeAsync_ReturnsViewWithEmptyList()
        {
            // Arrange
            var mockProductService = new Mock<IProductAPIService>();
            var products = new List<ProductVmDto>();

            mockProductService.Setup(service => service.GetAll(It.IsAny<QueryObject>()))
                .ReturnsAsync(products);

            var component = new ListProductNewViewComponent(mockProductService.Object);

            // Act
            var result = await component.InvokeAsync() as ViewViewComponentResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<IList<ProductVmDto>>(result.ViewData.Model);
            Assert.Empty(model);
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenProductServiceIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ListProductNewViewComponent(null));
        }
    }
}
