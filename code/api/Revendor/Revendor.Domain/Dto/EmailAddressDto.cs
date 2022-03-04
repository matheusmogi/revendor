using Revendor.Domain.ValueObjects;

namespace Revendor.Domain.Dto
{
    public class EmailAddressDto 
    {
        public EmailAddressDto()
        {
            
        }
        public EmailAddressDto(EmailAddress emailAddress)
        {
            if(emailAddress==null)
                return;
            
            Email = emailAddress.Email;
        }

        public string Email { get; set; }
    }
}