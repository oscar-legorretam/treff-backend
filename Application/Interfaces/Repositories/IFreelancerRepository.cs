using Application.Interfaces.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IFreelancerRepository : IRepository<Freelancer>
    {
        Task<Freelancer> GetFreelancerByIdAsync(int freelancerId);
        Task<List<Service>> GetAllServicesByFreelancerIdAsync(int freelancerId);
    }
}
