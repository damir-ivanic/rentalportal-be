using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentalportal.model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace rentalportal.data.ef.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
        }
    }
}
