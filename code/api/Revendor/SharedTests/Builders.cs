using System;
using Bogus;
using Revendor.Domain.Dto;
using Revendor.Domain.ValueObjects;

namespace SharedTests
{
    public static class Builders
    {
        public static UserDto GivenAnUserDto(string password = "Pa$$w0rd", string role = null)
        {
            return new Faker<UserDto>("pt_BR")
                .RuleFor(x => x.Id, a => a.Random.Guid().ToString())
                .RuleFor(x => x.Active, true)
                .RuleFor(x => x.Password, password)
                .RuleFor(x => x.Role, string.IsNullOrEmpty(role) ? "Admin" : role)
                .RuleFor(x => x.Username, s => s.Person.UserName)
                .Generate();
        }

        public static CustomerDto GivenACustomerDto()
        {
            var customerDto = new Faker<CustomerDto>("pt_BR")
                .RuleFor(a => a.Name, f => f.Name.FullName())
                .RuleFor(a => a.Email, f => f.Person.Email)
                .RuleFor(a => a.Address, f =>
                    new Faker<AddressDto>()
                        .RuleFor(a => a.AddressLine, f.Address.FullAddress)
                        .RuleFor(a => a.City, f.Address.City)
                        .RuleFor(a => a.Complement, f.Address.SecondaryAddress)
                        .RuleFor(a => a.Neighbourhood, f.Address.Direction())
                        .RuleFor(a => a.State, f.Address.State)
                        .RuleFor(a => a.StreetNumber, f.Address.BuildingNumber)
                        .RuleFor(a => a.ZipCode, f.Address.ZipCode())
                        .Generate()
                )
                .RuleFor(a => a.Birthday, f => f.Date.Past().ToString("yyyy-MM-dd"))
                .RuleFor(a => a.Id, f => f.Random.Guid().ToString())
                .RuleFor(a => a.TenantId, f => f.Random.Guid().ToString())
                .RuleFor(a => a.Notes, f => f.Lorem.Lines(5))
                .RuleFor(a => a.Tags, f => f.Lorem.Sentence(10).Split(" "))
                .RuleFor(a => a.PhoneNumbers, f =>
                    new Faker<PhoneDto>()
                        .RuleFor(a => a.PhoneNumber, e => e.Person.Phone)
                        .RuleFor(a => a.Label, e => e.Random.Word())
                        .Generate(1).ToArray()
                )
                .Generate();
            return customerDto;
        }

        public static ProductDto GivenAProductDto()
        {
            var productDto = new Faker<ProductDto>("pt_BR")
                .RuleFor(x => x.Code, e => e.Random.Guid().ToString())
                .RuleFor(x => x.Description, e => e.Lorem.Lines(2))
                .RuleFor(x => x.Name, e => e.Name.JobArea())
                .RuleFor(x => x.ValidUntil, e => e.Date.Future().ToString("yyyy-MM-dd"))
                .RuleFor(x => x.EAN, e => e.Random.Guid().ToString())
                .RuleFor(a => a.Id, f => f.Random.Guid().ToString())
                .RuleFor(a => a.TenantId, f => f.Random.Guid().ToString())
                .RuleFor(x => x.Brand, e => new Faker<BrandDto>("pt_BR")
                    .RuleFor(x => x.Id, f => f.Random.Guid().ToString())
                    .Generate())
                .Generate();
            return productDto;
        }
    }
}