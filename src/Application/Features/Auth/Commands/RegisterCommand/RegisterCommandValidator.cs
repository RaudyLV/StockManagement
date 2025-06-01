
using FluentValidation;

namespace Application.Features.Auth.Commands.RegisterCommand
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
             RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                // .EmailAddress().WithMessage("El formato del email no es valido.")
                .MaximumLength(60).WithMessage("El email no debe exceder los 60 caracteres.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .NotNull()
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage("La confirmacion de la contraseña es obligatorio.")
                .NotNull()
                .Equal(p => p.Password).WithMessage("Las contraseñas no son iguales.");
        }
    }
}