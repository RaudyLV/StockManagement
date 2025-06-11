using Application.DTOs;
using Application.Exceptions;
using Application.Features.Products.Queries;
using Application.Interfaces;
using Application.Specifications.Products;
using Core.Domain.Entities;

namespace Infraestructure.Persistence.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryAsync<Product> _repository;
        private readonly IInventoryMovementService _inventoryService;
        private readonly IBrandService _brandService;
        private readonly ICacheService _cacheService;

        public ProductService(IRepositoryAsync<Product> repository, IInventoryMovementService inventoryService, IBrandService brandService, ICacheService cacheService)
        {
            _repository = repository;
            _inventoryService = inventoryService;
            _brandService = brandService;
            _cacheService = cacheService;
        }

        public async Task AddAsync(Product product)
        {


            await _repository.BeginTransactionAsync();

            try
            {
                await _repository.AddAsync(product);

                await _inventoryService.AddProductMovementAsync(product);

                await _brandService.AddProductToListAsync(product.BrandId, product);


                await _repository.SaveChangesAsync();

                await _repository.CommitAsync();
            }
            catch
            {
                await _repository.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _repository.GetByIdAsync(product.Id);
            if (existingProduct == null)
                throw new NotFoundException("Producto no encontrado!");

            await _repository.BeginTransactionAsync();

            try
            {
                await _repository.UpdateAsync(product);
                
                await _inventoryService.AddInventoryUpdateMovement(product, existingProduct.Quantity);

                await _repository.SaveChangesAsync();

                await _repository.CommitAsync();
            }
            catch
            {
                await _repository.RollbackAsync();
                throw;
            }
        }
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException("Producto no encontrado!");

            return product;
        }

        public async Task<List<ProductDto>> GetProductsAsync(GetAllProductsQuery request)
        {
            string cacheKey = $"products:list{request.PageNumber}:{request.Name.ToLower() ?? "all"} ";

            var cached = await _cacheService.GetAsync<List<ProductDto>>(cacheKey);
            if (cached is not null) return cached;

            var products = await _repository.ListAsync(new PagedProductsSpec
            (
                request.PageSize,
                request.PageNumber,
                request.Name,
                request.BrandName,
                request.Price,
                request.Quantity
            ));

            await _cacheService.SetAsync(cacheKey, products, TimeSpan.FromMinutes(30));
            
            return products;
        }
        public async Task<ProductDto> GetProductByNameAsync(string name)
        {
            string cacheKey = $"product:byname:{name.ToLower()}";

            var cached = await _cacheService.GetAsync<ProductDto>(cacheKey);
            if(cached is not null) return cached;

            var product = await _repository.FirstOrDefaultAsync(new GetProductByNameSpec(name));

            await _cacheService.SetAsync(cacheKey, product, TimeSpan.FromHours(1));

            return product!;
        }

    }
}