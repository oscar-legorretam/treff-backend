using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Mapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ServiceFeatures.Commands
{
    public class CreateServiceCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string KeyWords { get; set; }

        public string Description { get; set; }

        public string MainImage { get; set; }
        public ICollection<Package> Packages { get; set; }

        public int FreelancerId { get; set; }
        public bool Highlight { get; set; } = false;

        public bool ExpressDelivery { get; set; } = false;
        public List<FileModel> Files { get; set; }
        public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, int>
        {
            private readonly IServiceRepository _context;
            public CreateServiceCommandHandler(IServiceRepository context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
            {
                var serviceEntitiy = ServiceMapper.Mapper.Map<Service>(command);

                serviceEntitiy.ServiceImages = new List<ServiceImage>();

                foreach (var formFile in command.Files)
                {
                    var serviceImage = new ServiceImage();
                    Guid guid = Guid.NewGuid();
                    var base64 = formFile.File;
                    var split = base64.Split(',');
                    base64 = split.Length > 1 ? split[1] : base64;
                    var path = AppDomain.CurrentDomain.BaseDirectory + @"Images\";
                    var name = guid.ToString() + formFile.FileName;

                    File.WriteAllBytes(path + name, Convert.FromBase64String(base64));
                    serviceImage.Image = name;
                    serviceEntitiy.ServiceImages.Add(serviceImage);

                }
                serviceEntitiy.MainImage = "";

                var response = await _context.AddAsync(serviceEntitiy);
                return 1;
            }
        }
    }

    public class FileModel
    {
        public int ServiceId { get; set; }

        public string File { get; set; }
        public string FileName { get; set; }

    }
}
