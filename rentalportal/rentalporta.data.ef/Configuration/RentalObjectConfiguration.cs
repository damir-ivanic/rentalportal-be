using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentalportal.model.Domain;

namespace rentalportal.data.ef.Configuration
{
    public class RentalObjectConfiguration : IEntityTypeConfiguration<RentalObject>
    {
        public void Configure(EntityTypeBuilder<RentalObject> builder)
        {
        }
    }
}
