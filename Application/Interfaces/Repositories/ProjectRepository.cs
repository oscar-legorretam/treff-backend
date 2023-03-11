using Application.Interfaces.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetActiveByFreelancerIdAsync(int freelancerId, int status);
        Task<Project> GetActiveByIdAsync(int id);
    }
}
