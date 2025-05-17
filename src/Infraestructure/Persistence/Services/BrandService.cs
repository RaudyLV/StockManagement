
using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.Brands;
using Core.Domain.Entities;

namespace Infraestructure.Persistence.Services
{
    public class BrandService : IBrandService
    {
        private readonly IRepositoryAsync<Brand> _repo;

        public BrandService(IRepositoryAsync<Brand> repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(Brand brand)
        {
            var existsBrand = await _repo.FirstOrDefaultAsync(new GetBrandByNameSpec(brand.Name));
            if(existsBrand != null)
                throw new AlreadyExistException($"La marca {brand.Name} ya existe!");
            
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
            var brand = await _repo.FirstOrDefaultAsync(new BrandByIdWithProductsSpec(id));
            if (brand == null)
                throw new NotFoundException("La marca no existe!");

            return brand;
                                    
        }

        public IQueryable<Brand> GetBrands()
        {
            throw new NotImplementedException();
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

        //Esta funcion ayuda a verificar si existe una marca con el mismo nombre ya
        public async Task<Brand> GetBrandByNameAsync(string name)
        {
            var existingBrand = await _repo.FirstOrDefaultAsync(new GetBrandByNameSpec(name));

            return existingBrand!;
        }
    }
}