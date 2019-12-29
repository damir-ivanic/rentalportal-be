using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using rentalportal.model.Core;
using rentalportal.model.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace rentalportal.data.ef
{
    public class DbContextUnitOfWork : IUnitOfWork
    {
        private readonly RentalPortalContext _context;
        //private readonly ITimeProvider _timeProvider;
        //private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ILogger<DbContextUnitOfWork> _logger;
        private readonly IConfiguration _configuration;

        private bool _cancelSaving;

        public DbContextUnitOfWork(RentalPortalContext context,
            //ITimeProvider timeProvider,
            //ICurrentUserProvider currentUserProvider,
            IConfiguration configuration,
            ILogger<DbContextUnitOfWork> logger)
        {
            _context = context;
            _configuration = configuration;
            //_timeProvider = timeProvider;
            //_currentUserProvider = currentUserProvider;
            _logger = logger;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_cancelSaving)
            {
                _logger.LogWarning("Not saving database changes since saving was cancelled.");
                return 0;
            }

            AuditEntities();

            int numberOfChanges = await _context.SaveChangesAsync(cancellationToken);
            _logger.LogDebug(
                $"{numberOfChanges} of changed were saved to database {_configuration.GetConnectionString("DefaultConnection")}");
            return numberOfChanges;
        }

        public int SaveChanges()
        {
            if (_cancelSaving)
            {
                _logger.LogWarning("Not saving database changes since saving was cancelled.");
                return 0;
            }

            AuditEntities();

            int numberOfChanges = _context.SaveChanges();
            _logger.LogDebug(
                $"{numberOfChanges} of changed were saved to database");
            return numberOfChanges;
        }

        public void CancelSaving()
        {
            _cancelSaving = true;
        }

        private void AuditEntities()
        {
            var changedEntities = _context.ChangeTracker.Entries<Entity>();

            foreach (var entity in changedEntities)
            {
                if (entity.State == EntityState.Added)
                {
                    //entity.Entity.SetCreated(_currentUserProvider.User, _timeProvider.Now);
                    //entity.Entity.SetUpdated(_currentUserProvider.User, _timeProvider.Now);
                }

                if (entity.IsModified())
                {
                    //entity.Entity.SetUpdated(_currentUserProvider.User, _timeProvider.Now);
                }
            }
        }
    }
}
