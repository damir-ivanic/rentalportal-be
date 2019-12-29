using MediatR;
using rentalportal.model.Domain.RentalObjects;
using System;
using System.Collections.Generic;

namespace rentalportal.domain.services.RentalObject.Queries
{
    public class RentalObjectOverviewQuery : IRequest<CommandResult<IEnumerable<RentalObjectOverview>>>
    {
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }
    }
}
