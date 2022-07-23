
using MonsterMexa.Domain;

namespace MonsterMexa.UnitTests
{
    public class ProductTests
    {
        [Fact]
        public void Create_IsValid_ShouldReturnProduct()
        {
            // arrange
            string name = "Test";
            int size = 26;

            // act
            var product = Product.Create(name, size);

            // assert
            Assert.True(product.IsSuccess);
            Assert.Equal("Test", product.Value.Name);
            Assert.Equal(26, product.Value.Size);
        }

        [Fact]
        public void Create_IsNotValid_ShouldReturnProduct()
        {
            // arrange
            string name = string.Empty;
            int size = 26;

            // act
            var product = Product.Create(name, size);

            // assert
            Assert.True(product.IsFailure);
            Assert.Equal("Name cannot be null or whitespace", product.Error);
        }
    }
}
