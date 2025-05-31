
using FluentValidation;

namespace Application.Features.Brands.Commands.CreateBrandCommand
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre no puede estar vacio.")
                .MaximumLength(56).WithMessage("El nombre no puedde ser mayor a 56 cáracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción no puede estar vacio.")
                .MaximumLength(256).WithMessage("La descripción no puedde ser mayor a 256 cáracteres.");

            
        }
    }
}