using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.CategoryFeatures.Queries;
using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using Application.Features.ServiceFeatures.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ServiceController : BaseApiController
    {
        /// <summary>
        /// Gets all Products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));
        }
        /// <summary>
        /// Gets Service Entity by Category Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("category/{id}")]
        public async Task<IActionResult> GetByCategoryId(int id)
        {
            return Ok(await Mediator.Send(new GetAllServicesByCategoryIdQuery { Id = id }));
        }
        /// <summary>
        /// Gets Service Entity by Category Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("categoryPremium/{id}")]
        public async Task<IActionResult> GetPremiumByCategoryId(int id)
        {
            return Ok(await Mediator.Send(new GetAllServicesPremiumByCategoryIdQuery { Id = id }));
        }

        /// <summary>
        /// Gets Service Entity by limit.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("highlightLimit/{limit}")]
        public async Task<IActionResult> GetPremiumLimitId(int limit)
        {
            return Ok(await Mediator.Send(new GetAllServicesPremiumLimitQuery { Limit = limit }));
        }

        /// <summary>
        /// Gets Service Entity by limit.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("limit/{limit}")]
        public async Task<IActionResult> GetLimitId(int limit)
        {
            return Ok(await Mediator.Send(new GetAllServicesLimitQuery { Limit = limit }));
        }

    }
}