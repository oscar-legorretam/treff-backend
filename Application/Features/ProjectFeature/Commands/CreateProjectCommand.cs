using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProjectFeatures.Commands
{
    public class CreateProjectCommand : IRequest<int>
    {
        public int FreelancerId { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public int PackageId { get; set; }
        public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
        {
            private readonly IProjectRepository _context;
            private readonly IServiceRepository _contextService;
            public CreateProjectCommandHandler(IProjectRepository context,
                IServiceRepository contextService)
            {
                _context = context;
                _contextService = contextService;
            }
            public async Task<int> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
            {
                var serviceAmount = 10;
                var projectEntitiy = ProjectMapper.Mapper.Map<Project>(command);
                var service = await _contextService.GetServiceByIdAsync(command.ServiceId);
                var package = service.Packages.Where(p => p.Id == command.PackageId).FirstOrDefault();

                projectEntitiy.CreationDate = DateTime.Now;
                projectEntitiy.CalculatedFinishDate = DateTime.Now.AddDays(package.Time);
                projectEntitiy.Price = package.Cost + serviceAmount;
                projectEntitiy.Finished = false;
                projectEntitiy.Status = 1;
                projectEntitiy.ChatId = Guid.NewGuid().ToString();

                var response = await _context.AddAsync(projectEntitiy);
                return response.Id;
            }
        }
    }
}
