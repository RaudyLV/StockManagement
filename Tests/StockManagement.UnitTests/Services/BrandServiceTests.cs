using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}