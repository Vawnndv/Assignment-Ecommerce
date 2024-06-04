using AutoFixture.Xunit2;
using AutoFixture;
using Ecommerce_Customers_Site.Components.Product;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Shared_ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce_Customers_Site.Services.Product;
using Moq;

namespace Ecommerce.UnitTest.ViewComponents
{
    public class ProductCardViewComponentTests
    {
        private readonly Fixture _fixture;

        public ProductCardViewComponentTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task InvokeAsync_ReturnsViewWithProduct()
        {
            // Arrange
            var product = _fixture.Create<ProductVmDto>();

            var viewComponent = new ProductCardViewComponent();

            // Act
            var result = await viewComponent.InvokeAsync(product) as ViewViewComponentResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<ProductVmDto>(result.ViewData.Model);
            Assert.Equal(product.Name, model.Name);
            Assert.Equal(product.Description, model.Description);
            Assert.Equal(product.Price, model.Price);
            Assert.Equal(product.Discount, model.Discount);
            Assert.Equal(product.CreatedDate, model.CreatedDate);
            Assert.Equal(product.UpdatedDate, model.UpdatedDate);
            Assert.Equal(product.CategoryId, model.CategoryId);
        }

        [Fact]
        public async Task InvokeAsync_ReturnsViewWithEmpty()
        {
            // Arrange
            var viewComponent = new ProductCardViewComponent();

            // Act
            var result = await viewComponent.InvokeAsync(null) as ViewViewComponentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ViewData.Model);
        }
    }
}
