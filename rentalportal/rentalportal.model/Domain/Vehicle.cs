using System;
using System.Collections.Generic;
using System.Text;

namespace rentalportal.model.Domain
{
    public class Vehicle : Entity
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Mileage { get; set; }
        public string Plates { get; set; }
        public string Sticker { get; set; }
        public DateTime Registered { get; set; }
        public DateTime Insured { get; set; }
        public DateTime Warranty { get; set; }
        public string Tires { get; set; }
        public bool Chains { get; set; }
        public string Color { get; set; }
    }
}
