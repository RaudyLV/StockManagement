
using Application.Exceptions;
using Application.Interfaces;
using Ardalis.Specification;
using Core.Domain.Entities;
using FluentAssertions;
using Infraestructure.Persistence.Services;
using Moq;
using Xunit;

namespace StockManagement.UnitTests.Services
{
    public class BrandServiceTests
    {
        private readonly Mock<IRepositoryAsync<Brand>> _mockRepo;
        private readonly BrandService _service;
        public BrandServiceTests()
        {
            _mockRepo = new();
            _service = new BrandService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetBrandById_ReturnsBrandsWithProducts()
        {
            //Arrange
            var id = Guid.NewGuid();
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Air Jordan 1", Description = "Tenis jordan", Quantity = 1, UnitPrice = 13.00, BrandId = id },
                new Product { Id = Guid.NewGuid(), Name = "Air Jordan 5", Description = "Tenis jordan", Quantity = 6, UnitPrice = 15.00, BrandId = id }
            };

            var expectedBrand = new Brand { Id = id, Name = "Nike", Description = "Tenis deportivos", Available = true, Products = products };

            _mockRepo.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Brand>>(), default))
            .ReturnsAsync(expectedBrand);


            //Act
            var result = await _service.GetBrandById(id);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Products.Should().HaveCount(2);

        }

        [Fact]
        public async Task UpdateBrand_ShouldUpdate_WhenNoConflict()
        {
            // Arrange
            var brandToUpdate = new Brand
            {
                Id = Guid.NewGuid(),
                Name = "Nike" 
            };

            _mockRepo.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Brand>>(), default))
                     .ReturnsAsync((Brand?)null);

            //Act
            await _service.UpdateAsync(brandToUpdate);

            //Arrange
            _mockRepo.Verify(repo => repo.UpdateAsync(brandToUpdate, default), Times.Once());   
            _mockRepo.Verify(repo => repo.SaveChangesAsync(default), Times.Once());   
        }

        [Fact]
        public async Task UpdateBrand_ShouldThrowException_WhenBrandDoesExist()
        {
            // Arrange
            var brandToUpdate = new Brand
            {
                Id = Guid.NewGuid(),
                Name = "Nike" // mismo nombre que la otra marca
            };

            var existingBRand = new Brand
            {
                Id = Guid.NewGuid(),
                Name = "Nike"
            };

            _mockRepo.Setup(r => r.FirstOrDefaultAsync(
                It.IsAny<ISpecification<Brand>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(existingBRand);

            //Act
            var result = async () => await _service.UpdateAsync(brandToUpdate);

            //Assert
            await result.Should().ThrowAsync<AlreadyExistException>();
        }
    }
}