using System.Data;
using System.Reflection.Metadata.Ecma335;
using FluentValidation;
using Revendor.Domain.Dto;

namespace Revendor.Domain.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.TenantId)
                .NotEmpty()
                .When(a => a.Role.ToLower() != "admin")
                .WithMessage("Tenant não informado.");
        }
    }
}