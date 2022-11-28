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
        Task<List<Service>> GetAllServicesByCategoryIdAsync(int categoryId);
        Task<List<Service>> GetAllServicesPremiumByCategoryIdAsync(int categoryId);
        Task<List<Service>> GetAllServicesPremiumAsync(int limit);
        Task<List<Service>> GetAllServicesAsync(int limit);
    }
}
