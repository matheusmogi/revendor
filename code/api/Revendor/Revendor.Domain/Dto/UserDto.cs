namespace Revendor.Domain.Dto
{
    public class UserDto 
    {
        public UserDto()
        {
            
        }

        public string Id { get; set; }
        public string TenantId { get; set; }
        public string Username { get;  set; }
        public string Password { get;  set; }
        public string Role { get;  set; }
        public bool Active { get;  set; }
    }
}