using Application.Interfaces.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<List<Package>> GetAllServicesByCategoryIdAsync(int categoryId);
        Task<List<Package>> GetAllServicesPremiumByCategoryIdAsync(int categoryId);
        Task<List<Package>> GetAllServicesPremiumAsync(int limit);
    }
}
