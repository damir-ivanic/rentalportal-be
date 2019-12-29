using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using rentalportal.api.Models;
using rentalportal.domain.services.RentalObject.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace rentalportal.api.Controllers
{
    [Route("api/[controller]")]
    public class RentalObjectsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public RentalObjectsController(IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("overview")]
        public async Task<ActionResult<IEnumerable<RentalObjectOverviewViewModel>>> GetRentalObjects([FromQuery]RentalObjectOverviewQuery query)
        {
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(_mapper.Map<IEnumerable<RentalObjectOverviewViewModel>>(result.Payload));
            }

            return BadRequest(result.FailureReason);
        }
    }
}
