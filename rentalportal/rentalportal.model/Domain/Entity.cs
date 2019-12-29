using System;
using System.Collections.Generic;
using System.Text;

namespace rentalportal.model.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
