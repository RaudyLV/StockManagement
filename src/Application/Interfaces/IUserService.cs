

using Core.Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task UpdateAsync(DomainUser user);
        Task AddAsync(DomainUser user);
    }
}