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
        public async Task<List<Service>> GetAllServicesByCategoryIdAsync(int categoryId)
        {
            var categories = await _treffContext.Categories
                .Where(c => c.Deleted == 0
                    && c.Parent == null
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

            return services;
        }

        public async Task<List<Service>> GetAllServicesPremiumByCategoryIdAsync(int categoryId)
        {
            var categories = await _treffContext.Categories
                .Where(c => c.Deleted == 0
                    && c.Parent == null
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

            return services;
        }

        public async Task<List<Service>> GetAllServicesPremiumAsync(int limit)
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

            return services;
        }

        public async Task<List<Service>> GetAllServicesAsync(int limit)
        {
            var services = await _treffContext.Services
                .Include(s => s.Freelancer)
                .Include(s => s.Packages)
                .Include(s => s.Category)
                .OrderBy(s => s.Id)
                .Take(limit)
                .ToListAsync();

            services.ForEach(x => x.Packages = x.Packages.OrderBy(y => y.Cost).ToList());

            return services;
        }

        private Service SortInclude(Service p)
        {
            p.Packages = (p.Packages as HashSet<Package>)?
                .OrderBy(s => s.Cost)
                .ToHashSet<Package>();
            return p;
        }
    }
}
