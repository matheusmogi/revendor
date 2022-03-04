using System.Collections.Generic;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;

namespace Revendor.Domain.ValueObjects
{
    public class EmailAddress : ValueObject
    {
        protected EmailAddress()
        {
        }

        public EmailAddress(EmailAddressDto emailAddressDto)
        {
            if (emailAddressDto == null)
                return;
            
            Email = emailAddressDto.Email;
        }

        public string Email { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
        }
    }
}