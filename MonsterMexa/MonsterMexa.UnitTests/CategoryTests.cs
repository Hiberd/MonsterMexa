using MonsterMexa.Domain;

namespace MonsterMexa.UnitTests
{
    public class CategoryTests
    {
        [Fact]
        public void Create_IsValid_ShouldReturnCategory()
        {
            // arrange
            string name = "Test";

            // act
            var category = Category.Create(name);

            // assert
            Assert.True(category.IsSuccess);
            Assert.Equal("Test", category.Value.Name);
        }

        [Fact]
        public void Create_IsNotValid_ShouldReturnCategory()
        {
            // arrange
            string name = string.Empty;

            // act
            var category = Category.Create(name);

            // assert
            Assert.True(category.IsFailure);
            Assert.Equal("Name cannot be null or whitespace", category.Error);
        }
    }
}
