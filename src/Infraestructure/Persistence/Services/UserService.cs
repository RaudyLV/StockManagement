using Application.Exceptions;
using Application.Interfaces;
using Core.Domain.Entities;
using Infraestructure.Persistence.Repositories;

namespace Infraestructure.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryAsync<DomainUser> _repository;

        public UserService(IRepositoryAsync<DomainUser> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(DomainUser user)
        {
            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(DomainUser user)
        {
            var existingUser = await _repository.GetByIdAsync(user.Id);
            if (existingUser == null)
                throw new NotFoundException("Usuario no encontrado!");

            await _repository.UpdateAsync(user);
            await _repository.SaveChangesAsync();
        }
    }
}