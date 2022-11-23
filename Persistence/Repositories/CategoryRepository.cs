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
    public class CategoryRepository : Repository<Categories>, ICategoryRepository
    {
        public CategoryRepository(treff_v2Context treffContext) : base(treffContext) { }

        public async Task<List<Categories>> GetAllCategoriesAsync()
        {
            var categories = _treffContext.Categories
                .Where(c => c.Deleted == 0)
                .Include(c => c.SubCategories)
                .ThenInclude(s => s.Category)
                .ThenInclude(s => s.SubCategories)
                .ThenInclude(s => s.Category);
            return await categories.ToListAsync();
        }
    }
}
