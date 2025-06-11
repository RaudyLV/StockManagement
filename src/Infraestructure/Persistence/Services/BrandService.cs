
using Application.DTOs;
using Application.Exceptions;
using Application.Features.Brands.Queries;
using Application.Interfaces;
using Application.Specifications.Brands;
using Azure;
using Core.Domain.Entities;

namespace Infraestructure.Persistence.Services
{
    public class BrandService : IBrandService
    {
        private readonly IRepositoryAsync<Brand> _repo;
        private readonly ICacheService _cacheService;
        public BrandService(IRepositoryAsync<Brand> repo, ICacheService cacheService)
        {
            _repo = repo;
            _cacheService = cacheService;
        }

        public async Task AddAsync(Brand brand)
        {
            var existsBrand = await GetBrandByNameAsync(brand.Name);
            
            if (existsBrand != null) throw new AlreadyExistException($"La marca {brand.Name} ya existe!");
            
            await _repo.AddAsync(brand);
            await _repo.SaveChangesAsync();
        }


        public async Task DeleteAsync(Brand brand)
        {
            var existsBrand = await _repo.GetByIdAsync(brand.Id);
            if (existsBrand == null)
                throw new NotFoundException($"La marca {brand.Name} no existe!");

            await _repo.DeleteAsync(brand);
            await _repo.SaveChangesAsync();
        }


        public async Task UpdateAsync(Brand brand)
        {
            var existingBrand = await GetBrandByNameAsync(brand.Name);
            if (existingBrand != null && existingBrand.Id != brand.Id)
                throw new AlreadyExistException($"La marca {brand.Name} ya existe ");

            await _repo.UpdateAsync(brand);
            await _repo.SaveChangesAsync();
        }
        public async Task<Brand> GetBrandById(Guid id)
        {
            var brand = await _repo.GetByIdAsync(id);
            if (brand == null)
                throw new NotFoundException("La marca no existe!");

            return brand;
                                    
        }
        public async Task<List<BrandDto>> GetBrandsAsync(GetAllBrandsQuery query)
        {
            string cacheKey = $"brand:list{query.PageNumber}:{query.brandName.ToLower() ?? "all"}";

            var cached = await _cacheService.GetAsync<List<BrandDto>>(cacheKey);
            if (cached is not null) return cached;

            var brands = await _repo.ListAsync(new PagedBrandsSpec(query.PageSize, query.PageNumber, query.brandName));

            await _cacheService.SetAsync(cacheKey, brands, TimeSpan.FromMinutes(30));

            return brands;
        }

        //Funcion para agregar una lista de producto a la marca correspondiente
        //Servira para filtrados y demas. 
        public async Task AddProductToListAsync(Guid BrandId, Product product)
        {
            var brand = await _repo.GetByIdAsync(BrandId);
            if (brand == null)
                throw new NotFoundException("La marca no existe!");

            brand.Products.Add(product);

            await _repo.UpdateAsync(brand);
            await _repo.SaveChangesAsync();
        }
            public async Task<BrandDto> GetBrandByNameAsync(string name)
            {
                string cacheKey = $"brands:byname:{name.ToLower()}";
                
                var cached = await _cacheService.GetAsync<BrandDto>(cacheKey);
                if (cached is not null) return cached;

                var existingBrand = await _repo.FirstOrDefaultAsync(new GetBrandByNameSpec(name));
                await _cacheService.SetAsync(cacheKey, existingBrand, TimeSpan.FromHours(1));

                return existingBrand!;
            }
    }
}