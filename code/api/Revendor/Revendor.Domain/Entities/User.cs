using System.Security.Cryptography;
using Revendor.Domain.Dto;
using Revendor.Domain.ValueObjects;

namespace Revendor.Domain.Entities
{
    public class User : TenantBasedEntity
    {
        protected User()
        {
        }

        public User(UserDto userDto) : base(userDto.Id, userDto.TenantId)
        {
            Username = userDto.Username;
            Password = userDto.Password;
            Role = userDto.Role?.ToLower() == "admin" ? Role.Admin : Role.Tenant;
            Active = userDto.Active;
        }

        public string Username { get;  set; }
        public string Password { get;  set; }
        public Role Role { get;  set; }
        public bool Active { get;  set; }
    }
    
}