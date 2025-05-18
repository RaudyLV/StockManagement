

using Application.Features.Brands.Commands.UpdateBrandCommand;
using Core.Domain.Entities;
using FluentValidation.TestHelper;
using Xunit;

public class UpdateBrandCommandValidatorTest
{
    private readonly UpdateBrandCommandValidator _validator;
    public UpdateBrandCommandValidatorTest()
    {
        _validator = new UpdateBrandCommandValidator();
    }

    //Funcion que valida que cuando se ingrese el nombre de la marca vacio ("")
        //Devuelva el error de validacion correctamente 
    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        //Arrange
        var command = new UpdateBrandCommand
        {
            Name = "",
        };

        //Act
        var validation = _validator.TestValidate(command);

        //Assert
        validation.ShouldHaveValidationErrorFor(x => x.Name);
    }

    //Verifica que lance un error cuando el nombre sea mayor a 50 cáracteres
    [Fact]
    public void Should_Have_Error_When_Name_Is_TooLong()
    {
        //Arrange
        var command = new UpdateBrandCommand
        {
            Name = new string('a', 57)
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    [Fact]
    public void Should_Have_Error_When_Description_Is_Empty()
    {
        //Arrange
        var command = new UpdateBrandCommand
        {
            Description = "",
        };

        //Act
        var validation = _validator.TestValidate(command);

        //Assert
        validation.ShouldHaveValidationErrorFor(x => x.Description);
    }
    //Funcion que valida que cuando se ingrese la descripcion de la marca vacio ("")
    //Devuelva el error de validacion correctamente 
    [Fact]
        public void Should_Have_Error_When_Description_Is_TooLong()
        {
            //Arrange
            var command = new UpdateBrandCommand
            {
                Description = new string('a', 257)
            };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }
 
}
