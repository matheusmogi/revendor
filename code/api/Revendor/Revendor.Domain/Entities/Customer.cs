using System;
using System.Collections.Generic;
using System.Linq;
using Revendor.Domain.Dto;
using Revendor.Domain.ValueObjects;

namespace Revendor.Domain.Entities
{
    public class Customer : TenantBasedEntity
    {
        protected Customer()
        {
        }

        public Customer(CustomerDto customerDto) : base(customerDto.Id, customerDto.TenantId)
        {
            Name = customerDto.Name;
            Cpf = customerDto.Cpf;
            Birthday = string.IsNullOrEmpty(customerDto.Birthday) ? default :DateTime.Parse(customerDto.Birthday);
            Address = new Address(customerDto.Address);
            Notes = customerDto.Notes;
            Tags = customerDto.Tags != null ? string.Join(",", customerDto.Tags) : null;
            Email = customerDto.Email;
            PhoneNumbers = customerDto.PhoneNumbers?.Select(e => new Phone(e)).ToList();
        }

        public string Name { get; private set; }
        public string Nickname { get; private set; }
        public string Avatar { get; private set; }
        public string Cpf { get; private set; }
        public Gender Gender { get; private set; }
        public DateTime? Birthday { get; private set; }
        public Address Address { get; private set; }
        public string Notes { get; private set; }
        public string Tags { get; private set; }
        public string Email { get; private set; }
        public IEnumerable<Phone> PhoneNumbers { get; private set; }

        public void Update(CustomerDto dto)
        {
            Name = dto.Name;
            Cpf = dto.Cpf;
            Birthday = string.IsNullOrEmpty(dto.Birthday) ? default : DateTime.Parse(dto.Birthday);
            Address = new Address(dto.Address);
            Notes = dto.Notes;
            Tags = dto.Tags != null ? string.Join(",", dto.Tags) : null;
            Email = dto.Email;
            PhoneNumbers = dto.PhoneNumbers?.Select(e => new Phone(e)).ToList();
        }
    }
}