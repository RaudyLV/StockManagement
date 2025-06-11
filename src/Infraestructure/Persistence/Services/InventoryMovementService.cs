

using Application.DTOs;
using Application.Features.InventoryMovements;
using Application.Interfaces;
using Application.Specifications.Movements;
using Core.Domain.Entities;

namespace Infraestructure.Persistence.Services
{
    public class InventoryMovementService : IInventoryMovementService
    {
        private readonly IRepositoryAsync<InventoryMovements> _repo; 
        private readonly ICacheService _cacheService;
        public InventoryMovementService(IRepositoryAsync<InventoryMovements> repo, ICacheService cacheService)
        {
            _repo = repo;
            _cacheService = cacheService;
        }


        public async Task AddProductMovementAsync(Product product)
        {
            var movement = new InventoryMovements
            {
                ProductId = product.Id,
                Change = product.Quantity,
                Type =  "In",
                Reason = "Compra",
                Timestamp = DateTime.UtcNow
            };

            await _repo.AddAsync(movement);
            await _repo.SaveChangesAsync();
        }
        public async Task AddInventoryUpdateMovement(Product product, int originalQuantity)
        {
            int change = product.Quantity - originalQuantity;

            var movement = new InventoryMovements
            {
                ProductId = product.Id,
                Change = change,
                Type = change > 0 ? "In" : "Out",
                Reason = change > 0 ? "Compra" : "Venta",
                Timestamp = DateTime.UtcNow
            };

            await _repo.AddAsync(movement);
            await _repo.SaveChangesAsync();
        }

        public async Task<List<InventoryMovementsDto>> GetAllMovementsAsync(GetAllMovementsQuery query)
        {
            string cacheKey = $"movements:list{query.PageNumber}:{query.ProductName.ToLower() ?? "all"}";

            var cached = await _cacheService.GetAsync<List<InventoryMovementsDto>>(cacheKey);
            if (cached is not null) return cached;

            var movements = await _repo.ListAsync(new PagedMovementsSpec
            (
                query.PageSize,
                query.PageNumber,
                query.Reason,
                query.ProductName
            ));

            await _cacheService.SetAsync(cacheKey, movements, TimeSpan.FromHours(1));

            return movements;
        }

        public Task<InventoryMovements> GetMovementsByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}