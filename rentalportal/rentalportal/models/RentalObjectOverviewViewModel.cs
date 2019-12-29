using System;
using System.Collections.Generic;

namespace rentalportal.api.Models
{
    public class RentalObjectOverviewViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ReservationViewModel> Reservations { get; set; }
    }
}
