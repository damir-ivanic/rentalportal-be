using AutoMapper;
using rentalportal.domain.services.Reservations.Commands;
using rentalportal.model.Domain;

namespace rentalportal.domain.services.Reservations.Mapping.TypeConverters
{
    public class AddReservatoinTypeConverter : ITypeConverter<AddReservationCommand, Reservation>
    {
        public Reservation Convert(AddReservationCommand source, Reservation destination, ResolutionContext context)
        {
            return new Reservation
            {
                From = source.From,
                To = source.To,
                CustomerId = source.CustomerId,
                RentalObjectId = source.RentalObjectId
            };
        }
    }
}
