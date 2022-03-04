using System;
using System.Linq;
using Revendor.Domain.Entities;

namespace Revendor.Domain.Dto
{
    public class CustomerDto : TenantBaseDto
    {
        public CustomerDto()
        {
        }

        public CustomerDto(Customer customer)
        {
            Id = customer.Id;
            Cpf = customer.Cpf;
            Address = new AddressDto(customer.Address);
            Birthday = customer.Birthday == DateTime.MinValue ? null : customer.Birthday?.ToString("yyyy-MM-dd");
            Name = customer.Name;
            Notes = customer.Notes;
            Tags = customer.Tags?.Split(",");
            PhoneNumbers = customer.PhoneNumbers.Select(a => new PhoneDto(a)).ToArray();
            Email = customer.Email;
            TenantId = customer.TenantId;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Cpf { get; set; }
        public AddressDto Address { get; set; }
        public string Notes { get; set; }
        public string[] Tags { get; set; }
        public string Email { get; set; }
        public PhoneDto[] PhoneNumbers { get; set; }
    }
}