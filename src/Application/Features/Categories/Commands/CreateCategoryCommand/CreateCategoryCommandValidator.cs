
using FluentValidation;

namespace Application.Features.Categories.Commands.CreateCategoryCommand
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("El nombre no puede estar vacio.")
                    .MaximumLength(56).WithMessage("El nombre no puedde ser mayor a 56 cáracteres.");

            RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("La descripción no puede estar vacio.")
                    .MaximumLength(256).WithMessage("La descripción no puedde ser mayor a 256 cáracteres.");

            RuleFor(x => x.isAvailable)
                    .NotEmpty().WithMessage("Indique si se encuentra o no disponible.");
        }        
    }
}