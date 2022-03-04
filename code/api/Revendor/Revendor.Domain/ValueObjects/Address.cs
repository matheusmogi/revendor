using System.Collections.Generic;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;

namespace Revendor.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        protected Address()
        {
            
        }
        public Address(AddressDto addressDto)
        {
            if(addressDto == null)
                return;

            AddressLine = addressDto.AddressLine;
            StreetNumber = addressDto.StreetNumber;
            Complement = addressDto.Complement;
            Neighbourhood = addressDto.Neighbourhood;
            City = addressDto.City;
            State = addressDto.State;
            ZipCode = addressDto.ZipCode;
        }
        public string AddressLine { get; private set; }
        public string StreetNumber { get; private set; }
        public string Complement { get; private set; }
        public string  Neighbourhood { get; private set; }
        public string  City { get; private set; }
        public string  State { get; private set; }
        public string  ZipCode { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return $"{AddressLine} {StreetNumber} {Complement} {Neighbourhood} {City} {State} {ZipCode}";
        }
    }
}