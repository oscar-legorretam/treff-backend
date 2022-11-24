using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Context;
using Persistence.Repositories.Base;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(treff_v2Context treffContext) : base(treffContext) { }

        public async Task<List<Package>> GetAllServicesByCategoryIdAsync(int categoryId)
        {
            //var services = _treffContext.Services
            //    .Where(s => s.CategoryId == categoryId)
            //    .Include(c => c.Packages);
            //return await services.ToListAsync();
            var services = _treffContext.Packages
                .Where(s => s.Service.CategoryId == categoryId)
                .OrderByDescending(s => s.Premium)
                .Include(s => s.Service)
                .ThenInclude(s => s.Freelancer);
            return await services.ToListAsync();
        }

        public async Task<List<Package>> GetAllServicesPremiumByCategoryIdAsync(int categoryId)
        {
            //var services = _treffContext.Services
            //    .Where(s => s.CategoryId == categoryId)
            //    //.Where(s => s.Packages.Where(p => p.Premium == true))
            //    .Include(c => c.Packages
            //        .Where(p => p.Premium == true));
            //return await services.ToListAsync();
            var services = _treffContext.Packages
                .Where(s => s.Service.CategoryId == categoryId
                    && s.Premium == true)
                .Include(s => s.Service)
                .ThenInclude(s => s.Freelancer); ;
            return await services.ToListAsync();
        }

        public async Task<List<Package>> GetAllServicesPremiumAsync(int limit)
        {
            //var services = _treffContext.Services
            //    .Where(s => s.CategoryId == categoryId)
            //    //.Where(s => s.Packages.Where(p => p.Premium == true))
            //    .Include(c => c.Packages
            //        .Where(p => p.Premium == true));
            //return await services.ToListAsync();
            var services = _treffContext.Packages
                .Where(s =>  s.Premium == true)
                .Include(s => s.Service)
                .ThenInclude(s => s.Freelancer)
                .Include(s => s.Service)
                .ThenInclude(s => s.Category)
                .OrderBy(s => s.Id)
                .Take(limit);
            return await services.ToListAsync();
        }
    }
}
