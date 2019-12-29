using AutoMapper;
using rentalportal.domain.services.Reservations.Commands;
using rentalportal.domain.services.Reservations.Mapping.TypeConverters;
using rentalportal.model.Domain;

namespace rentalportal.domain.services.Reservations.Mapping
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<AddReservationCommand, Reservation>()
                .ConvertUsing<AddReservatoinTypeConverter>();
        }
    }
}
