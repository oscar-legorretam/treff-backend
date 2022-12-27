using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Mapper;
using Application.Utils;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Application.Features.FreelancerFeatures.Commands
{
    public class LoginFreelancerCommand : IRequest<Freelancer>
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public class LoginFreelancerCommandHandler : IRequestHandler<LoginFreelancerCommand, Freelancer>
        {
            private readonly IFreelancerRepository _context;
            public LoginFreelancerCommandHandler(IFreelancerRepository context)
            {
                _context = context;
            }
            public async Task<Freelancer> Handle(LoginFreelancerCommand request, CancellationToken cancellationToken)
            {
                var freelancerEntitiy = FreelancerMapper.Mapper.Map<Freelancer>(request);
                freelancerEntitiy.Password = request.Password.EncodeToBase64();
                var response = await _context.LoginAsync(freelancerEntitiy);

                if (response == null)
                {
                    throw new UnauthorizedAccessException();
                }

                return response;
            }
        }
    }
}
