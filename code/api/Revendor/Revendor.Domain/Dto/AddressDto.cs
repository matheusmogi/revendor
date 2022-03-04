using Revendor.Domain.ValueObjects;

namespace Revendor.Domain.Dto
{
    public class AddressDto
    {
        public AddressDto()
        {
        }

        public AddressDto(Address address)
        {
            if (address == null)
                return;

            AddressLine = address.AddressLine;
            StreetNumber = address.StreetNumber;
            Complement = address.Complement;
            Neighbourhood = address.Neighbourhood;
            City = address.City;
            State = address.State;
            ZipCode = address.ZipCode;
        }

        public string AddressLine { get; set; }
        public string StreetNumber { get; set; }
        public string Complement { get; set; }
        public string Neighbourhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}