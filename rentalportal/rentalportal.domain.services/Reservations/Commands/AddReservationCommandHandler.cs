using AutoMapper;
using MediatR;
using rentalportal.model.Core;
using rentalportal.model.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace rentalportal.domain.services.Reservations.Commands
{
    public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, CommandResult<NoResult>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Reservation> _reservationRepository;

        public AddReservationCommandHandler(IMapper mapper,
            IRepository<Reservation> reservationRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }

        public Task<CommandResult<NoResult>> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = _mapper.Map<Reservation>(request);
            _reservationRepository.Add(reservation);
            return Task.FromResult(CommandResult<NoResult>.Success(new NoResult()));
        }
    }
}
