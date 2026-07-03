using FluentValidation;
using backend.Models.Auth;

public class LoginDtoValidator : AbstractValidator<LoginDTO>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}