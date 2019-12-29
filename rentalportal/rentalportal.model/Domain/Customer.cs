using System;
using System.Collections.Generic;
using System.Text;

namespace rentalportal.model.Domain
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public virtual IEnumerable<Reservation> Reservations { get; set; }
    }
}
