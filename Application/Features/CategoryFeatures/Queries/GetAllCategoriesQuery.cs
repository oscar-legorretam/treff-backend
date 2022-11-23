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

namespace Application.Features.CategoryFeatures.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<Categories>>
    {

        public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Categories>>
        {
            private readonly ICategoryRepository _context;
            public GetAllCategoriesQueryHandler(ICategoryRepository context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Categories>> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
            {
                var productList = await _context.GetAllCategoriesAsync();
                if (productList == null)
                {
                    return null;
                }
                return productList.AsReadOnly();
            }
        }
    }
}
