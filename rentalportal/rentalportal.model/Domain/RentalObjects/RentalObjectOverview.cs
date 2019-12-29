using System;
using System.Collections.Generic;
using System.Text;

namespace rentalportal.model.Domain.RentalObjects
{
    public class RentalObjectOverview
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
