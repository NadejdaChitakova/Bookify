using FluentAssertions;

namespace Bookify.Domain.UnitTests.Users
{
    public class UserTests
    {
        [Fact]
        public void Create_Should_SetPropertyValues()
        {
            //Act
            var user = Domain.Users.User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

            //Assert
            user.FirstName.Should().Be(UserData.FirstName);
            user.LastName.Should().Be(UserData.LastName);
            user.Email.Should().Be(UserData.Email);
        }
    }
}
