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

        public ProductService(IRepositoryAsync<Product> repository, IInventoryMovementService inventoryService, IBrandService brandService)
        {
            _repository = repository;
            _inventoryService = inventoryService;
            _brandService = brandService;
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


        public Task DeleteAsync(Product product)
        {
            throw new NotImplementedException();
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

        public async Task<ICollection<Product>> GetProductsAsync(GetAllProductsQuery request)
        {
            return await _repository.ListAsync(new PagedProductsSpec
            (
                request.PageSize,
                request.PageNumber,
                request.Name,
                request.BrandName,
                request.Price,
                request.Quantity
            ));
        }
        public async Task<ProductDto> GetProductByNameAsync(string name)
        {
            var product = await _repository.FirstOrDefaultAsync(new GetProductByNameSpec(name));

            return product!;
        }

    }
}