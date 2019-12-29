using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentalportal.model.Domain;

namespace rentalportal.data.ef.Configuration
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.From).IsRequired();
            builder.Property(x => x.To).IsRequired();

            builder.HasOne(x => x.RentalObject)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.RentalObjectId);
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.CustomerId);
        }
    }
}
