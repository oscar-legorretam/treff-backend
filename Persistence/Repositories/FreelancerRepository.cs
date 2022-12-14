using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Context;
using Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class FreelancerRepository : Repository<Freelancer>, IFreelancerRepository
    {
        public FreelancerRepository(treff_v2Context treffContext) : base(treffContext) { }

        public async Task<Freelancer> GetFreelancerByIdAsync(int freelancerId)
        {
            var freelancer = await _treffContext.Freelancers
                .Include(s => s.FreelancerComments)
                .ThenInclude(c => c.Comment)
                .ThenInclude(u => u.User)
                .Where(s => s.Id == freelancerId)
                .FirstOrDefaultAsync();


            return freelancer;
        }
        public async Task<List<Service>> GetAllServicesByFreelancerIdAsync(int freelancerId)
        {
            var services = await _treffContext.Services
                .Include(s => s.Freelancer)
                .Include(s => s.Category)
                .Include(s => s.Packages)
                .Where(s => s.FreelancerId == freelancerId)
                .ToListAsync();

            services.ForEach(x => x.Packages = x.Packages.OrderBy(y => y.Cost).ToList());

            return services;
        }
    }
}
