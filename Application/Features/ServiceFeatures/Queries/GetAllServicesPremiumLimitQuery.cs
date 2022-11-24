using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ServiceFeatures.Queries
{
    public class GetAllServicesPremiumLimitQuery : IRequest<IEnumerable<Package>>
    {
        public int Limit { get; set; }
        public class GetAllServicesPremiumLimitQueryHandler : IRequestHandler<GetAllServicesPremiumLimitQuery, IEnumerable<Package>>
        {
            private readonly IServiceRepository _context;
            public GetAllServicesPremiumLimitQueryHandler(IServiceRepository context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Package>> Handle(GetAllServicesPremiumLimitQuery query, CancellationToken cancellationToken)
            {
                var serviceList = await _context.GetAllServicesPremiumAsync(query.Limit);
                if (serviceList == null)
                {
                    return null;
                }
                return serviceList.AsReadOnly();
            }
        }
    }
}
