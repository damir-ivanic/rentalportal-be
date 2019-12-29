using Microsoft.EntityFrameworkCore;
using rentalportal.model.Domain;
using System;
using System.Linq;
using System.Reflection;

namespace rentalportal.data.ef
{
    public class RentalPortalContext : DbContext
    {
        public RentalPortalContext(DbContextOptions<RentalPortalContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var currentAssembly = GetType().Assembly;
            RegisterConfigurations(modelBuilder);
            RegisterEntities(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void RegisterConfigurations(ModelBuilder modelBuilder)
        {
            var currentAssembly = GetType().Assembly;

            var configurations = currentAssembly.DefinedTypes
                .Where(t => 
                    t.ImplementedInterfaces.Any(i => i.IsGenericType && i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.InvariantCultureIgnoreCase)) && 
                    t.IsClass &&
                    !t.IsAbstract &&
                    !t.IsNested)
                .ToList();

            foreach (var configuration in configurations)
            {
                var entityType = configuration.ImplementedInterfaces.First().GenericTypeArguments.SingleOrDefault(t => t.IsClass);

                var applyConfigMethod = typeof(ModelBuilder).GetMethods().Single(x => x.Name == "ApplyConfiguration" && x.GetParameters().First().ParameterType.Name == typeof(IEntityTypeConfiguration<>).Name);

                var applyConfigGenericMethod = applyConfigMethod.MakeGenericMethod(entityType);

                applyConfigGenericMethod.Invoke(modelBuilder,
                        new object[] { Activator.CreateInstance(configuration) });
            }
        }

        private void RegisterEntities(ModelBuilder modelBuilder)
        {
            var currentAssembly = GetType().Assembly;

            var entityMethod = typeof(ModelBuilder).GetMethod("Entity", new Type[] { });

            var entityTypes = Assembly.GetAssembly(typeof(Entity)).GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Entity)) && !x.IsAbstract);

            foreach (var type in entityTypes)
            {
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }

    }
}
