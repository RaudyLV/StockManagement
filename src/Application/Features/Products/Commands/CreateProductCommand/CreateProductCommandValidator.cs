
using Application.Interfaces;
using Core.Domain.Entities;
using FluentValidation;

namespace Application.Features.Products.Commands.CreateProductCommand
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre no puede estar vacio.")
                .MaximumLength(25).WithMessage("El nombre no puedde ser mayor a 25 cáracteres.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción no puede estar vacio.")
                .MaximumLength(256).WithMessage("La descripción no puedde ser mayor a 256 cáracteres.");

            RuleFor(p => p.Quantity)
                    .NotNull().WithMessage("La cantidad no puede ser null")
                    .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");

            RuleFor(p => p.UnitPrice)
                    .NotNull().WithMessage("El precio unitario no puede ser null")
                    .GreaterThan(0).WithMessage("El precio unitario debe ser mayor a 0");


        }
    }
}