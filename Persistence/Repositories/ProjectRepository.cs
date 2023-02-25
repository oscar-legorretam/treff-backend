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

        public async Task<IEnumerable<Project>> GetActiveByFreelancerIdAsync(int freelancerId)
        {
            return await _treffContext.Projects
                .Where(p => p.FreelancerId == freelancerId)
                .ToListAsync();
        }

    }
}
