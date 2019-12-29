using System;
using System.Linq;

namespace rentalportal.model.Domain.RentalObjects
{
    public static class RentalObjectQueryExtensions
    {
        public static IQueryable<RentalObjectOverview> GetOverview(this IQueryable<Reservation> query, DateTimeOffset from, DateTimeOffset to)
        {
            return query.Where(x => x.From.Date >= from.Date && x.To.Date <= to.Date)
                .GroupBy(x => new { x.RentalObject.Id, x.RentalObject.Name })
                .Select(x => new RentalObjectOverview
                {
                    Id = x.Key.Id,
                    Name = x.Key.Name,
                    Reservations = x.Select(r => new Reservation
                    {
                        From = r.From,
                        To = r.To
                    })
                });
        }
    }
}
