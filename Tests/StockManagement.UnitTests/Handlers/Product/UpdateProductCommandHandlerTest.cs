
using Application.Features.Products.Commands.UpdateProductCommand;
using Application.Interfaces;
using AutoMapper;
using Core.Domain.Entities;
using FluentAssertions;

using Moq;
using Xunit;

namespace StockManagement.UnitTests
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IProductService> _productServiceMock = new();
        private readonly Mock<IBrandService> _brandServiceMock = new();
        private readonly Mock<ICategoryService> _categoryServiceMock = new();

        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _handler = new UpdateProductCommandHandler(
                _mapperMock.Object,
                _productServiceMock.Object,
                _brandServiceMock.Object,
                _categoryServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateProduct_WhenDataIsValid()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                BrandId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Name = "Test Product"
            };

            _productServiceMock.Setup(x => x.GetProductByIdAsync(command.Id))
                .ReturnsAsync(new Product());

            _brandServiceMock.Setup(x => x.GetBrandById(command.BrandId))
                .ReturnsAsync(new Brand());

            var mappedProduct = new Product { Id = command.Id, Name = command.Name };
            _mapperMock.Setup(x => x.Map<Product>(command)).Returns(mappedProduct);

            _productServiceMock.Setup(x => x.UpdateAsync(mappedProduct))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.Should().Be(command.Id);
            result.Message.Should().Be("Producto actualizado correctamente");
        }

        [Fact]
        public async Task Handle_ShouldPreserveQuantityAndPrice_WhenOnlyNameIsUpdated()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var originalProduct = new Product
            {
                Id = productId,
                Name = "Nombre viejo",
                Quantity = 50,
                UnitPrice = 100.0
            };

            var command = new UpdateProductCommand
            {
                Id = productId,
                BrandId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Name = "Nuevo nombre"
                //aqui el unitprice y cantidad(quantity) deberian ser los mismos que el original
            };

            _productServiceMock.Setup(x => x.GetProductByIdAsync(productId))
                .ReturnsAsync(originalProduct);

            _brandServiceMock.Setup(x => x.GetBrandById(command.BrandId))
                .ReturnsAsync(new Brand());

            _mapperMock.Setup(x => x.Map<Product>(command))
                .Returns(new Product
                {
                    Id = productId,
                    Name = command.Name,
                    Quantity = originalProduct.Quantity,
                        UnitPrice = originalProduct.UnitPrice
                });

            Product? savedProduct = null;

            _productServiceMock.Setup(x => x.UpdateAsync(It.IsAny<Product>()))
                .Callback<Product>(p => savedProduct = p)
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            savedProduct.Should().NotBeNull();
            savedProduct!.Name.Should().Be(command.Name);
            savedProduct.Quantity.Should().Be(originalProduct.Quantity);
            savedProduct.UnitPrice.Should().Be(originalProduct.UnitPrice);
        }

    
    }

}