using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
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
        public DateTime CreationDate { get; set; }
        public DateTime CalculatedFinishDate { get; set; }
        public DateTime FinishDate { get; set; }
        public double Price { get; set; }
        public bool Finished { get; set; }
        public int Status { get; set; }
        public string Receipt { get; set; }
        public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
        {
            private readonly IServiceRepository _context;
            private readonly IAzureBlobService _contextStorage;
            public CreateProjectCommandHandler(IServiceRepository context, IAzureBlobService contextStorage)
            {
                _context = context;
                _contextStorage = contextStorage;
            }
            public async Task<int> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
            {
                var serviceEntitiy = ServiceMapper.Mapper.Map<Service>(command);

                var response = await _context.AddAsync(serviceEntitiy);
                return response.Id;
            }
        }
    }
}
