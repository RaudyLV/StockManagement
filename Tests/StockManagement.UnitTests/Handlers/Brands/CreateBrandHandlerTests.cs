
using Application.Features.Brands.Commands.CreateBrandCommand;
using Application.Interfaces;
using AutoMapper;
using Core.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace StockManagement.UnitTests.Handlers.Brands
{
    public class CreateBrandHandlerTests
    {
        private readonly Mock<IBrandService> _mockRepo;
        private readonly IMapper _mapper;
        private readonly CreateBrandCommandHandler _handler;
        public CreateBrandHandlerTests()
        {
            _mockRepo = new Mock<IBrandService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateBrandCommand, Brand>();
            });

            _mapper = config.CreateMapper();

            _handler = new CreateBrandCommandHandler(_mapper, _mockRepo.Object);
        }

        [Fact]
        public async Task Handle_CreateBrandSuccessfully()
        {
            //Arrange
            var command = new CreateBrandCommand { Name = "Nike", Description = "Tenis nike", Available = true };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            result.Should().NotBe("");
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<Brand>()), Times.Once());  
        }
    }
}