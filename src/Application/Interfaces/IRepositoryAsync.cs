using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Application.Interfaces
{
    public interface IRepositoryAsync<T> : IRepositoryBase<T>  where T : class
    {
        
    }
}