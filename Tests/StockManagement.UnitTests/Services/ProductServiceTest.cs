


using Application.Interfaces;
using Core.Domain.Entities;
using Infraestructure.Persistence.Services;
using Moq;
using Xunit;

namespace StockManagement.UnitTests.Services
{
    public class ProductServiceTest
    {
        private readonly Mock<IRepositoryAsync<Product>> _mockRepo;
        private readonly Mock<IRepositoryAsync<InventoryMovements>> _mockMovementRepo;
        private readonly Mock<IRepositoryAsync<Brand>> _brandRepo;
        private readonly ProductService _productService;
        private readonly InventoryMovementService _movementService;
        private readonly BrandService _brandService;
        public ProductServiceTest()
        {
            _mockRepo = new Mock<IRepositoryAsync<Product>>();

            _mockMovementRepo = new Mock<IRepositoryAsync<InventoryMovements>>();

            _brandRepo = new();

            _movementService = new InventoryMovementService(_mockMovementRepo.Object);

            _brandService = new(_brandRepo.Object);

            _productService = new ProductService(_mockRepo.Object, _movementService, _brandService);
        }

        [Fact]
        public async Task AddProduct_ShouldAddProduct_WithoutErrors()
        {
            //Arrange

            var brand = new Brand { Id = Guid.NewGuid() };

            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                BrandId = brand.Id,
                Name = "Air Force 1"
            };
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>(), default))
                 .ReturnsAsync(newProduct);

            _mockRepo.Setup(r => r.GetByIdAsync(newProduct.Id, default)).ReturnsAsync(newProduct);

            _brandRepo.Setup(r => r.GetByIdAsync(brand.Id, default)).ReturnsAsync(brand);

            //Act
            await _productService.AddAsync(newProduct);

            //Assert
            _mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p => p == newProduct), default), Times.Once());
        }

    }
}