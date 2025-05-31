

using Application.DTOs;
using Core.Domain.Entities;

namespace Application.Interfaces
{
    public interface IBrandService
    {
        IQueryable<Brand> GetBrands();
        Task<Brand> GetBrandById(Guid id);
        Task AddAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task DeleteAsync(Brand brand);
        Task AddProductToListAsync(Guid BrandId, Product product);
        Task<BrandDto> GetBrandByNameAsync(string name);
    }
}