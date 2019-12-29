using MediatR;
using System;

namespace rentalportal.domain.services.Reservations.Commands
{
    public class AddReservationCommand : IRequest<CommandResult<NoResult>>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid RentalObjectId { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
