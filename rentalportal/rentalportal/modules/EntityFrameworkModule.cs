using Autofac;
using rentalportal.data.ef;
using rentalportal.model.Core;
using System;
using System.Linq;

namespace rentalportal.api.modules
{
    public class EntityFrameworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IRepository<>).Assembly)
                .Where(
                    type =>
                        ImplementsInterface(typeof(IRepository<>), type) ||
                        type.Name.EndsWith("Repository")
                ).AsImplementedInterfaces().InstancePerLifetimeScope();

            

            builder.RegisterType<RentalPortalContext>()
                //.WithParameter("isMigration", false)
                .InstancePerLifetimeScope();
            builder.RegisterType<DbContextUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }

        private bool ImplementsInterface(Type interfaceType, Type concreteType)
        {
            return
                concreteType.GetInterfaces().Any(
                    t =>
                        (interfaceType.IsGenericTypeDefinition && t.IsGenericType
                            ? t.GetGenericTypeDefinition()
                            : t) == interfaceType);
        }
    }
}
