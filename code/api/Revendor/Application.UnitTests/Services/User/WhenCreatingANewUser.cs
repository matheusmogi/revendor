using System;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Moq;
using NUnit.Framework;
using Revendor.Application.Services;
using Revendor.Domain.ValueObjects;
using SharedTests;

namespace Application.UnitTests.Services.User
{
    [TestFixture]
    public class WhenCreatingANewUser : ServiceTestBase<UserService>
    {
        protected override UserService Service { get; set; }

        protected override void AdditionalSetup()
        {
            Service = new UserService(Repository.Object, null, Logger.Object);
        }

        [Test]
        public async Task ShouldAddNewUser()
        {
            var userDto = Builders.GivenAnUserDto(password: "Pa$$w0rd");

            var result = await Service.CreateNewUser(userDto);

            Assert.True(result.Succeeded);
            Repository.Verify(x => x.AddAsync(It.Is<Revendor.Domain.Entities.User>(c =>
                c.Id == userDto.Id &&
                c.TenantId == userDto.TenantId &&
                BCrypt.Net.BCrypt.Verify("Pa$$w0rd", c.Password, false, HashType.SHA384) &&
                c.Role == Enum.Parse<Role>(userDto.Role) &&
                c.Username == userDto.Username &&
                c.Active == userDto.Active
            )));
        }


        [Test]
        public async Task ShouldReturnErrorWhenSomethingFail()
        {
            Repository.Setup(x => x.SaveChangesAsync()).Throws<Exception>();
            var userDto = Builders.GivenAnUserDto();

            var result = await Service.CreateNewUser(userDto);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Erro ao criar um novo usuário. Por favor, contate o suporte."));
        }

        [Test]
        public async Task ShouldValidateWhenTenantIdIsNullAndRoleIsNotAdmin()
        {
            var userDto = Builders.GivenAnUserDto(role: Role.Tenant.ToString());

            var result = await Service.CreateNewUser(userDto);

            Assert.False(result.Succeeded);
            Assert.True(result.Errors.Any(a => a == "Tenant não informado."));
        }
    }
}