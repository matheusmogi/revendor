using System.Threading.Tasks;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;

namespace Revendor.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<Result> CreateNewUser(UserDto userDto);
        Task<Result> UpdateUser(UserDto userDto);
        Result SignInUser(string username, string password);
        Task<Result> DeleteUser(string userId);
    }
}