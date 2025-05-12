using Application.Interfaces;
using Infraestructure.Persistence.Context;
using Ardalis.Specification.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class RepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        public RepositoryAsync(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}