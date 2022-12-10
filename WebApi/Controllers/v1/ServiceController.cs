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
        /// Gets all Categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Test()
        {
            return Ok("hola mundo Devops! ");
        }
        /// <summary>
        /// Gets all Categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));
        }
        /// <summary>
        /// Gets Service Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetServiceByIdQuery { Id = id }));
        }
        /// <summary>
        /// Gets Service Entity by Category Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("category")]
        public async Task<IActionResult> GetByCategoryId(GetAllServicesByCategoryIdQuery command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets Service Entity by Category Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("categoryPremium")]
        public async Task<IActionResult> GetPremiumByCategoryId(GetAllServicesPremiumByCategoryIdQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Gets Service Entity by limit.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("highlightLimit")]
        public async Task<IActionResult> GetPremiumLimitId(GetAllServicesPremiumLimitQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Gets Service Entity by limit.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("limit")]
        public async Task<IActionResult> GetLimitId(GetAllServicesLimitQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}