
using Application.Exceptions;
using Application.Interfaces;
using Core.Domain.Entities;

namespace Infraestructure.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryAsync<Category> _repo;

        public CategoryService(IRepositoryAsync<Category> repo)
        {
            _repo = repo;
        }
        public Task<List<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
          var category = await _repo.GetByIdAsync(id);

          if(category is null)
            throw new NotFoundException("Categoria no encontrada");

            return category;
        }

        public async Task AddAsync(Category category)
        {
            var existsCategory = await _repo.GetByIdAsync(category.Id);
            if  (existsCategory != null)
                throw new AlreadyExistException($"La categoria {category.Name} ya existe.");
            
            await _repo.AddAsync(category);
            await _repo.SaveChangesAsync();
        }


        public async Task UpdateAsync(Category category)
        {
            await _repo.UpdateAsync(category);
            await _repo.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid Id)
        {
            var category = await GetByIdAsync(Id);

            if(category is null)
            throw new NotFoundException("Categoria no encontrada");

            await _repo.DeleteAsync(category);
            await _repo.SaveChangesAsync();
        }

    }
}