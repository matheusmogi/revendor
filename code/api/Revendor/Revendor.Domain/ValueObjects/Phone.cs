using System.Collections.Generic;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;

namespace Revendor.Domain.ValueObjects
{
    public class Phone : ValueObject
    {
        protected Phone()
        {
            
        }
        public Phone(PhoneDto phoneDto)
        {
            PhoneNumber = phoneDto.PhoneNumber;
            Label = phoneDto.Label;
        }

        public string PhoneNumber { get; set; }
        public string Label { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PhoneNumber;
        }
    }
}