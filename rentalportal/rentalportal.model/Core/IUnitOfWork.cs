using System.Threading;
using System.Threading.Tasks;

namespace rentalportal.model.Core
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges();
        void CancelSaving();
    }
}
