
using Application.DTOs;
using Application.Features.Products.Queries;
using Core.Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ICollection<Product>> GetProductsAsync(GetAllProductsQuery request);
        Task<Product> GetProductByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task DeleteAsync(Product product);
        Task UpdateAsync(Product product);
        Task<ProductDto> GetProductByNameAsync(string name);    
    }
}