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
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(treff_v2Context treffContext) : base(treffContext) { }

        public async Task<IEnumerable<Project>> GetActiveByFreelancerIdAsync(int freelancerId, int status)
        {
            return await _treffContext.Projects
                .Include(p => p.Freelancer)
                .Include(p => p.User)
                .Include(p => p.Package)
                .Include(p => p.Service)
                .ThenInclude(s => s.Category)
                .Where(p => p.FreelancerId == freelancerId && 
                    p.Status == (Status)status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetActiveByUserIdAsync(int userId, int status)
        {
            return await _treffContext.Projects
                .Include(p => p.Freelancer)
                .Include(p => p.User)
                .Include(p => p.Package)
                .Include(p => p.Service)
                .ThenInclude(s => s.Category)
                .Where(p => p.UserId == userId &&
                    p.Status == (Status)status)
                .ToListAsync();
        }

        public async Task<Project> GetActiveByIdAsync(int id)
        {
            return await _treffContext.Projects
                .Include(p => p.Freelancer)
                .Include(p => p.User)
                .Include(p => p.Package)
                .Include(p => p.Service)
                .ThenInclude(s => s.Category)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

    }
}
