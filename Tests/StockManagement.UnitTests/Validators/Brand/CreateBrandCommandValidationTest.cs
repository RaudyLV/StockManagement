

using Application.Features.Brands.Commands.CreateBrandCommand;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace StockManagement.UnitTests.Validators.Brand
{
    public class CreateBrandCommandValidationTest
    {
        private readonly CreateBrandCommandValidator _validator;
        public CreateBrandCommandValidationTest()
        {
            _validator = new CreateBrandCommandValidator();
        }

        //Funcion que valida que cuando se ingrese el nombre de la marca vacio ("")
        //Devuelva el error de validacion correctamente 
        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            //Arrange
            var command = new CreateBrandCommand
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
            var command = new CreateBrandCommand
            {
                Name = new string('a', 57)
            };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        //Funcion que valida que cuando se ingrese la descripcion de la marca vacio ("")
        //Devuelva el error de validacion correctamente 
        [Fact]
        public void Should_Have_Error_When_Description_Is_Empty()
        {
            //Arrange
            var command = new CreateBrandCommand
            {
                Description = "",
            };

            //Act
            var validation = _validator.TestValidate(command);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.Description);
        }

        //Verifica que lance un error cuando la descripcion sea mayor a 256 cáracteres
        [Fact]
        public void Should_Have_Error_When_Description_Is_TooLong()
        {
            //Arrange
            var command = new CreateBrandCommand
            {
                Description = new string('a', 257)
            };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }
    }
}