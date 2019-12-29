using MediatR;
using Microsoft.AspNetCore.Mvc;
using rentalportal.api.Filters;
using rentalportal.domain.services;
using rentalportal.domain.services.Reservations.Commands;
using System.Threading.Tasks;

namespace rentalportal.api.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [TransactionFilter]
        public async Task<ActionResult<NoResult>> Post([FromBody] AddReservationCommand command)
        {
            CommandResult<NoResult> result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Payload);
            }

            return BadRequest(result.FailureReason);
        }
    }
}
