using System;

namespace rentalportal.model.Domain
{
    public class Reservation : Entity
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid RentalObjectId { get; set; }
        public Guid? CustomerId { get; set; }
        public virtual RentalObject RentalObject { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
