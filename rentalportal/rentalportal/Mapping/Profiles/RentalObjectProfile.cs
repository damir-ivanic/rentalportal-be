using AutoMapper;
using rentalportal.api.Models;
using rentalportal.model.Domain;
using rentalportal.model.Domain.RentalObjects;

namespace rentalportal.api.mapping.profiles
{
    public class RentalObjectProfile : Profile
    {
        public RentalObjectProfile()
        {
            CreateMap<RentalObjectOverview, RentalObjectOverviewViewModel>();
            //CreateMap<Reservation, ReservationViewModel>();
        }
    }
}
