using System;

namespace rentalportal.api.Models
{
    public class ReservationViewModel
    {
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }
    }
}
