using Revendor.Domain.ValueObjects;

namespace Revendor.Domain.Dto
{
    public class PhoneDto 
    {
        public PhoneDto()
        {
            
        }
        public PhoneDto(Phone phone)
        {
            PhoneNumber = phone.PhoneNumber;
            Label = phone.Label;
        }

        public string PhoneNumber { get; set; }
        public string Label { get; set; }
    }
}