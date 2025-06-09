

using Application.DTOs;
using Application.Features.Brands.Queries;
using Core.Domain.Entities;

namespace Application.Interfaces
{
    public interface IBrandService
    {
        Task<ICollection<Brand>> GetBrandsAsync(GetAllBrandsQuery query);
        Task<Brand> GetBrandById(Guid id);
        Task AddAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task DeleteAsync(Brand brand);
        Task AddProductToListAsync(Guid BrandId, Product product);
        Task<BrandDto> GetBrandByNameAsync(string name);
    }
}