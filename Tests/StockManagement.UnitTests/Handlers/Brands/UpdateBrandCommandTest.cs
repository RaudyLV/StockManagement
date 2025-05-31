

using Application.Features.Brands.Commands.UpdateBrandCommand;
using Application.Interfaces;
using AutoMapper;
using Core.Domain.Entities;
using FluentAssertions;
using Moq;

namespace StockManagement.UnitTests.Handlers.Brands
{
    public class UpdateBrandCommandTest
    {
        private readonly Mock<IBrandService> _mockService;
        private readonly IMapper _mapper;
        private readonly UpdateBrandCommandHandler _handler;

        public UpdateBrandCommandTest()
        {
            _mockService = new Mock<IBrandService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateBrandCommand, Brand>();
            });

            _mapper = config.CreateMapper();

            _handler = new UpdateBrandCommandHandler(_mockService.Object, _mapper);
        }

        public async Task UpdateCommandHandler_Should_ExecuteSuccesfully()
        {
            //Arrange
            var existingBrand = new Brand
            {
                Id = Guid.NewGuid(),
                Name = "Nombre viejo",
                Description = "Vieja descripcion",
                IsAvailable = true
            };

            var command = new UpdateBrandCommand
            {
                Id = existingBrand.Id,
                Name = "Nombre nuevo",
                Description = "Nueva descripcion",
                isAvailable = false
            };

            
            _mockService.Setup(s => s.GetBrandById(command.Id))
                        .ReturnsAsync(existingBrand);
            //Act
            var handle = await _handler.Handle(command, default);

            //Arrange
            existingBrand.Name.Should().Be("Nombre nuevo");
            existingBrand.Description.Should().Be("Nueva descripcion");
            existingBrand.IsAvailable.Should().Be(false);

            _mockService.Verify(s => s.UpdateAsync(existingBrand), Times.Once());  
        }
    }
}