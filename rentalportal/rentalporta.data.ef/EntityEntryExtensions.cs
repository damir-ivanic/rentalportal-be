using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace rentalportal.data.ef
{
    public static class EntityEntryExtensions
    {
        public static bool IsModified(this EntityEntry entry) =>
            entry.State == EntityState.Modified ||
            entry.References.Any(r => r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned() && IsModified(r.TargetEntry));
    }
}
