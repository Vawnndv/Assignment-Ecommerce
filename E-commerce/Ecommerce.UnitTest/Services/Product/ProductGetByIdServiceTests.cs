using AutoFixture;
using Ecommerce_Customers_Site.Services.Product;
using Moq;
using Shared_ViewModels.Helpers;
using Shared_ViewModels.Product;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.UnitTest.Services.Product
{
    public class ProductGetByIdServiceTests
    {
        private readonly Fixture _fixture;

        public ProductGetByIdServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetById_ReturnsProduct_WhenIdIsValid()
        {
            // Arrange
            var productId = 1;
            var product = _fixture.Create<ProductVmDto>();
            var mockProductService = new Mock<IProductAPIService>();

            mockProductService.Setup(service => service.GetById(productId))
                .ReturnsAsync(product);

            // Act
            var result = await mockProductService.Object.GetById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNull_WhenIdIsInvalid()
        {
            // Arrange
            var productId = -1;
            var mockProductService = new Mock<IProductAPIService>();

            mockProductService.Setup(service => service.GetById(productId))
                .ReturnsAsync((ProductVmDto)null); // Return null for invalid ID

            // Act
            var result = await mockProductService.Object.GetById(productId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetById_ReturnsNull_WhenErrorResponse()
        {
            // Arrange
            var productId = 1;
            var mockProductService = new Mock<IProductAPIService>();

            mockProductService.Setup(service => service.GetById(productId))
                .ReturnsAsync((ProductVmDto)null); // Return null in case of server error

            // Act
            var result = await mockProductService.Object.GetById(productId);

            // Assert
            Assert.Null(result);
        }
    }
}
