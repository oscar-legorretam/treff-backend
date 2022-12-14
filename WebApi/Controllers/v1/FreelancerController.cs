using System.Threading.Tasks;
using Application.Features.FreelancerFeatures.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FreelancerController : BaseApiController
    {
        /// <summary>
        /// Gets Freelancer Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetFreelancerByIdQuery { Id = id }));
        }
        /// <summary>
        /// Gets Services Entity by Freelancer Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("services/{id}")]
        public async Task<IActionResult> GetByCategoryId(int id)
        {
            return Ok(await Mediator.Send(new GetAllServicesByFreelancerIdQuery { FreelancerId = id}));
        }
    }
}