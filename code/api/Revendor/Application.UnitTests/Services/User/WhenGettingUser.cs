using System.Linq;
using Moq;
using NUnit.Framework;
using Revendor.Application.Authentication;
using Revendor.Application.Services;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using SharedTests;

namespace Application.UnitTests.Services.User
{
    [TestFixture]
    public class WhenGettingUser : ServiceTestBase<UserService>
    {
        private readonly UserDto userDto = Builders.GivenAnUserDto();
        private string password;
        private Mock<ITokenIssuer> tokenIssuer;
        protected override UserService Service { get; set; }

        protected override void AdditionalSetup()
        {
            tokenIssuer = new Mock<ITokenIssuer>();
            tokenIssuer.Setup(x => x.IssueTokenForUser(It.IsAny<Credentials>(), It.IsAny<string>())).Returns("ACCESS-TOKEN");
            password = "Pa$$w0rd";
            userDto.Password = BCrypt.Net.BCrypt.HashPassword(password);
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.User>())
                .Returns(new[] { new Revendor.Domain.Entities.User(userDto) }.AsQueryable());
            Service = new UserService(Repository.Object, tokenIssuer.Object, Logger.Object);
        }

        [Test]
        public void ShouldGetCustomersFromRepository()
        {
            Service.SignInUser(userDto.Username, password);

            Repository.Verify(x => x.Query<Revendor.Domain.Entities.User>());
        }

        [Test]
        public void ShouldReturnUnauthorizedWhenUserIsNotFound()
        {
            var response = Service.SignInUser(userDto.Username, "wrong-password");

            Assert.False(response.Succeeded);
            Assert.That(response.Errors.First(), Is.EqualTo("Usuário não encontrado."));
        }

        [Test]
        public void ShouldReturnUserNotActive()
        {
            userDto.Active = false;
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.User>())
                .Returns(new[] { new Revendor.Domain.Entities.User(userDto) }.AsQueryable());
            Service = new UserService(Repository.Object, null, Logger.Object);

            var response = Service.SignInUser(userDto.Username, password);

            Assert.False(response.Succeeded);
            Assert.That(response.Errors.First(), Is.EqualTo("Usuário inativo."));
        }

        [Test]
        public void ShouldCallTokenIssuerToEncryptToken()
        {
            Service.SignInUser(userDto.Username, password);

            tokenIssuer.Verify(x => x.IssueTokenForUser(It.Is<Credentials>(a => a.User == userDto.Username), userDto.TenantId));
        }

        [Test]
        public void ShouldReturnToken()
        {
            var response = Service.SignInUser(userDto.Username, password);

            Assert.True(response.Succeeded);
            Assert.That(((AccessResponse)response.ResponseObject).AccessToken, Is.EqualTo("ACCESS-TOKEN"));
        }
    }
}