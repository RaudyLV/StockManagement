
using Application.Interfaces;
using Core.Domain.Entities;
using Infraestructure.Persistence.Services;
using Moq;
using Xunit;

namespace StockManagement.UnitTests.Services
{
    public class InventoryMovementServiceTest
    {
        private readonly Mock<IRepositoryAsync<InventoryMovements>> _mockRepo;
        private readonly InventoryMovementService _movementService;
        public InventoryMovementServiceTest()
        {
            _mockRepo = new Mock<IRepositoryAsync<InventoryMovements>>();

            _movementService = new InventoryMovementService(_mockRepo.Object);
        }

        //Aqui se prueba si se puede agregar un registro cuando el tipo de movimiento sea "IN" 
        //es decir, que sea cuando se agregue un producto.
        [Fact]
        public async Task Can_Add_Movement_WithType_In()
        {
            //Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Air Force 1",
                Quantity = 70,
                UnitPrice = 40.00
            };

            var inventoryMovement = new InventoryMovements
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Change = product.Quantity,
                Type = "In",
                Reason = "Compra",
                Timestamp = DateTime.UtcNow,
            };

            _mockRepo.Setup(x => x.AddAsync(It.IsAny<InventoryMovements>(), default))
                     .ReturnsAsync(inventoryMovement);
            //Act
            await _movementService.AddProductMovementAsync(product);

            //Assert
            _mockRepo.Verify(x => x.AddAsync(It.Is<InventoryMovements>(m =>
                m.Change == 70 &&
                m.Type == "In" &&
                m.Reason == "Compra"
            ), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateMovement_Should_Add_Inventory_Movement_When_ProductQuantity_Change()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existingProduct = new Product
            {
                Id = productId,
                Name = "Producto A",
                Quantity = 50,
                UnitPrice = 10
            };

            var updatedProduct = new Product
            {
                Id = productId,
                Name = "Producto A",
                Quantity = 30, // Cambio de cantidad
                UnitPrice = 10
            };

            int change = updatedProduct.Quantity - existingProduct.Quantity;

            var inventoryMovement = new InventoryMovements
            {
                Id = Guid.NewGuid(),
                ProductId = updatedProduct.Id,
                Change = change,
                Type = change > 0 ? "In" : "Out",
                Reason = change > 0 ? "Compra" : "Venta",
            };

            InventoryMovements? added = null!;

            _mockRepo.Setup(x => x.AddAsync(It.IsAny<InventoryMovements>(), default))
                     .Callback<InventoryMovements, CancellationToken>((m, _) => added = m)
                     .ReturnsAsync(inventoryMovement);

            // Act
            await _movementService.AddInventoryUpdateMovement(updatedProduct, existingProduct.Quantity);

            //Assert
            Assert.NotNull(added);
            Assert.Equal(updatedProduct.Id, existingProduct.Id);
            Assert.Equal(-20, change);
            Assert.Equal("Out", added.Type);
            Assert.Equal("Venta", added.Reason);
        }
    }
}