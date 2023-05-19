
using gearshop_dotnetapp.Services.ProductServices;

namespace GearShopTest
{
    internal class ServiceTest
    {

        [Fact]
        public void GetProductById_Should_Return_Correct_Product()
        {
            // Arrange
            var productService = new ProductService();

            // Act
            var product = productService.GetById(1);

            // Assert
            Assert.NotNull(product);
            Assert.Equal("Product A", product.Name);
            Assert.Equal(100.0, product.Price);
        }
    }
}
}
