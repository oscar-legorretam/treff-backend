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

        private List<int> getCategoryIds(List<Category> categories)
        {
            var catIds = new List<int>();

            foreach (var category in categories)
            {
                catIds.Add(category.Id);

                if (category.SubCategories != null)
                {
                    var subIds = getCategoryIds(category.SubCategories);
                    if (subIds.Count > 0)
                    {
                        catIds = catIds.Concat(subIds).ToList();
                    }
                }
            }
            return catIds;
        }

        public async Task<List<Service>> GetAllServicesByCategoryIdAsync(int categoryId, bool byFreelance = false)
        {
            var categories = await _treffContext.Categories
                .Where(c => c.Deleted == 0
                    //&& c.Parent == null
                    && c.Id == categoryId)
                .Include(c => c.SubCategories)
                .ThenInclude(s => s.SubCategories)
                .ThenInclude(s => s.Parent)
                .ToListAsync();

            var catIds = getCategoryIds(categories);

            var services = await _treffContext.Services
                .Include(s => s.Freelancer)
                .Include(s => s.Packages)
                .Include(s => s.Category)
                .OrderBy(s => s.Id)
                .Where(s => catIds.Contains(s.CategoryId))
                .ToListAsync();

            services.ForEach(x => x.Packages = x.Packages.OrderBy(y => y.Cost).ToList());

            services.ForEach(s =>
                {
                    s.Category.SubCategories = null;
                    s.Category.Parent = null;
                }
            );

            if (!byFreelance)
            {
                return services;
            }
            else
            {
                return services.GroupBy(s => s.FreelancerId)
                .Select(s => s.First()).ToList();
            }
        }

        public async Task<List<Service>> GetAllServicesPremiumByCategoryIdAsync(int categoryId, bool byFreelance = false)
        {
            var categories = await _treffContext.Categories
                .Where(c => c.Deleted == 0
                    //&& c.Parent == null
                    && c.Id == categoryId)
                .Include(c => c.SubCategories)
                .ThenInclude(s => s.SubCategories)
                .ThenInclude(s => s.Parent)
                .ToListAsync();

            var catIds = getCategoryIds(categories);

            var services = await _treffContext.Services
                .Include(s => s.Freelancer)
                .Include(s => s.Packages)
                .Include(s => s.Category)
                .OrderBy(s => s.Id)
                .Where(s => catIds.Contains(s.CategoryId)
                    && s.Highlight == true)
                .ToListAsync();

            services.ForEach(x => x.Packages = x.Packages.OrderBy(y => y.Cost).ToList());

            services.ForEach(s =>
            {
                s.Category.SubCategories = null;
                s.Category.Parent = null;
            }
            );

            if (!byFreelance)
            {
                return services;
            }
            else
            {
                return services.GroupBy(s => s.FreelancerId)
                .Select(s => s.First()).ToList();
            }
        }

        public async Task<List<Service>> GetAllServicesPremiumAsync(int limit, bool byFreelance = false)
        {
            var services = await _treffContext.Services
                .Where(s => s.Highlight == true)
                .Include(s => s.Freelancer)
                .Include(s => s.Packages)
                .Include(s => s.Category)
                .OrderBy(s => s.Id)
                .Take(limit)
                .ToListAsync();

            services.ForEach(x => x.Packages = x.Packages.OrderBy(y => y.Cost).ToList());

            if (!byFreelance)
            {
                return services;
            }
            else
            {
                return services.GroupBy(s => s.FreelancerId)
                .Select(s => s.First()).ToList();
            }
        }

        public async Task<List<Service>> GetAllServicesAsync(int limit, bool byFreelance = false)
        {
            var services = await _treffContext.Services
                .Include(s => s.Freelancer)
                .Include(s => s.Packages)
                .Include(s => s.Category)
                .OrderBy(s => s.Id)
                .Take(limit)
                .ToListAsync();

            services.ForEach(x => x.Packages = x.Packages.OrderBy(y => y.Cost).ToList());

            if (!byFreelance)
            {
                return services;
            }
            else
            {
                return services.GroupBy(s => s.FreelancerId)
                .Select(s => s.First()).ToList();
            }
        }

        public async Task<Service> GetServiceByIdAsync(int id)
        {
            var service = await _treffContext.Services
                .Include(s => s.Freelancer)
                .ThenInclude(f => f.FreelancerComments)
                .ThenInclude(c => c.Comment)
                .ThenInclude(u => u.User)
                .Include(s => s.Packages)
                .Include(s => s.Category)
                .Include(s => s.ServiceImages)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            service.Packages = service.Packages.OrderBy(y => y.Cost).ToList();

            return service;
        }
    }
}
