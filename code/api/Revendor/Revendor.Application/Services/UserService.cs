using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Revendor.Application.Authentication;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using Revendor.Domain.Entities;
using Revendor.Domain.Interfaces;
using Revendor.Domain.Interfaces.Services;
using Revendor.Domain.Validators;

namespace Revendor.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;
        private readonly ITokenIssuer issuerToken;
        private readonly ILogger<UserService> logger;
        private readonly UserDtoValidator validator;

        public UserService(IRepository repository, ITokenIssuer issuerToken, ILogger<UserService> logger)
        {
            validator = new UserDtoValidator();
            this.repository = repository;
            this.issuerToken = issuerToken;
            this.logger = logger;
        }

        public async Task<Result> CreateNewUser(UserDto userDto)
        {
            try
            {
                var result = await validator.ValidateAsync(userDto);
                if (!result.IsValid)
                {
                    return Result.Failure(result.Errors.Select(x => x.ErrorMessage));
                }

                userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                var user = new User(userDto);
                await repository.AddAsync(user);
                await repository.SaveChangesAsync();
                return Result.Success(userDto);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return Result.Failure("Erro ao criar um novo usuário. Por favor, contate o suporte.");
            }
        }

        public Task<Result> UpdateUser(UserDto userDto)
        {
            throw new System.NotImplementedException();
        }

        public Result SignInUser(string username, string password)
        {
            var users = repository.Query<User>().Where(x => x.Username == username).AsEnumerable();
            var user = users.FirstOrDefault(x => BCrypt.Net.BCrypt.Verify(password, x.Password));
            if (user == null)
            {
                return Result.Failure("Usuário não encontrado.");
            }

            if (!user.Active)
            {
                return Result.Failure("Usuário inativo.");
            }

            var token = issuerToken.IssueTokenForUser(new Credentials(user.Username, user.Role), user.TenantId);
            return Result.Success(new AccessResponse(token));
        }

        public Task<Result> DeleteUser(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}