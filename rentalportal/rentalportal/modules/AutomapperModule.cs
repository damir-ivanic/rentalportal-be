using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using rentalportal.domain.services.Reservations.Mapping.TypeConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace rentalportal.api.modules
{
    public class AutomapperModule : Autofac.Module
    {
        public Assembly[] Assemblies { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            new List<Type>
            {
                typeof (ITypeConverter<,>),
                typeof (IValueResolver<,,>),
                typeof (IMappingAction<,>)
            }.ForEach(type =>
                {
                    builder.RegisterAssemblyTypes(typeof(AutomapperModule).Assembly)
                      .AsClosedTypesOf(type)
                      .AsSelf()
                      .InstancePerRequest();

                    builder.RegisterAssemblyTypes(typeof(AddReservatoinTypeConverter).Assembly)
                      .AsClosedTypesOf(type)
                      .AsSelf()
                      .InstancePerRequest();
                });

            builder.RegisterAssemblyTypes(typeof(AutomapperModule).Assembly).As<Profile>();
            builder.RegisterAssemblyTypes(typeof(AddReservatoinTypeConverter).Assembly).As<Profile>();

            builder.Register(context =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                    {
                        cfg.AddProfile(profile);
                    }
                });
                config.AssertConfigurationIsValid();

                return config;
            }).AsSelf().SingleInstance();

            //builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
            //    .As<IMapper>()
            //    .InstancePerRequest();

            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.ConstructServicesUsing(type =>
            //    {
            //        var owinContext = _httpContextAccessor.HttpContext.Authentication;
            //        return owinContext.GetAutofacLifetimeScope().Resolve(type);
            //    });

            //    foreach (var profile in profiles)
            //    {
            //        cfg.AddProfile(profile);
            //    }
            //});


            //builder.RegisterInstance(config);

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>().SingleInstance();
        }
    }
}