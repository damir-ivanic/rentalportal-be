using System.Collections.Generic;

namespace rentalportal.model.Domain
{
    public class RentalObject : Entity
    {
        public string Name { get; set; }
        public virtual IEnumerable<Reservation> Reservations { get; set; }
        public virtual IEnumerable<Note> Notes { get; set; }

    }
}
