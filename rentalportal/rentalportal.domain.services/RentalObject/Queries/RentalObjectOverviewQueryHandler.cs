using AutoMapper;
using MediatR;
using rentalportal.model.Core;
using rentalportal.model.Domain;
using rentalportal.model.Domain.RentalObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace rentalportal.domain.services.RentalObject.Queries
{
    public class RentalObjectOverviewQueryHandler : IRequestHandler<RentalObjectOverviewQuery, CommandResult<IEnumerable<RentalObjectOverview>>>
    {
        private readonly IRepository<Reservation> _reservationRepository;

        public RentalObjectOverviewQueryHandler(IRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public Task<CommandResult<IEnumerable<RentalObjectOverview>>> Handle(RentalObjectOverviewQuery request, CancellationToken cancellationToken)
        {
            var reservations = _reservationRepository.ItemsNoTracking
                .GetOverview(request.From, request.To);
            return Task.FromResult(CommandResult<IEnumerable<RentalObjectOverview>>.Success(reservations));
        }
    }
}
