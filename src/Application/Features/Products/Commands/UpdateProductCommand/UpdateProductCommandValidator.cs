
using FluentValidation;

namespace Application.Features.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("El id del producto no puede estar vacio");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad no puede ser negativa.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.");
        }
    }
}