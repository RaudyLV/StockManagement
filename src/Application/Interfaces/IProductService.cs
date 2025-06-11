
using Application.DTOs;
using Application.Features.Products.Queries;
using Core.Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsAsync(GetAllProductsQuery request);
        Task<Product> GetProductByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task<ProductDto> GetProductByNameAsync(string name);    
    }
}