using Autofac;
using Autofac.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rentalportal.api.Configuration;
using rentalportal.api.modules;
using rentalportal.data.ef;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace rentalportal
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration,
            IHostingEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling
                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<RentalPortalContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("rentalportal.data.ef.migrations")));

            services.AddCors(options =>
            {
                //options.AddPolicy("AllowLocalAndDeployed",
                //    b => b.SetIsOriginAllowedToAllowWildcardSubdomains()
                //                      .WithOrigins("http://localhost:3000",
                //                                   "https://rentalclient.azurewebsites.net")
                //                      .AllowAnyMethod()
                //                      .WithHeaders("Access-Control-Allow-Headers", "Cache-Control", "Pragma", "Origin", "Authorization", "Content-Type", "X-Requested-With")
                //                      .Build());
            });
            services.ConfigureMediatR();

            // TODO dodati environment
            if (_environment.IsDevelopment())
            {
                //services.AddDevTestAuthentication();
                services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "rentalportal.api", Version = "v1" }); });
            }

            var builder = new ContainerBuilder();
            builder.RegisterModule(new EntityFrameworkModule());
            builder.RegisterModule(new AutomapperModule());
            builder.Populate(services);
            ApplicationContainer = builder.Build();
            //ConfigureAuth(services);
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "rentalportal.api");
                    c.RoutePrefix = String.Empty;
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }

        //private void ConfigureAuth(IServiceCollection services)
        //{
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //          .AddJwtBearer(jwtOptions =>
        //          {
        //              jwtOptions.Authority = $"https://login.microsoftonline.com/tfp/{Configuration["auth:aadb2c:Tenant"]}/{Configuration["auth:aadb2c:Policy"]}";
        //              jwtOptions.Audience = Configuration["auth:aadb2c:ClientId"];
        //              jwtOptions.Events = new JwtBearerEvents
        //              {
        //                  OnAuthenticationFailed = AuthenticationFailed
        //              };
        //          });

        //    services.AddAuthorization(options =>
        //    {
        //        options.AddPolicy("Scopes", policy =>
        //                           policy.RequireAssertion(context => context.User
        //                                                                   .HasClaim(c => c.Type == "http://schemas.microsoft.com/identity/claims/scope" &&
        //                                                                                  c.Value.Contains(Configuration["auth:aadb2c:ScopeRead"]) &&
        //                                                                                  c.Value.Contains(Configuration["auth:aadb2c:ScopeWrite"]))));

        //        options.AddPolicy("ClaimThatDoesNotExist", policy =>
        //                            policy.RequireClaim("Non existant claim"));
        //    });
        //}

        //private Task AuthenticationFailed(AuthenticationFailedContext arg)
        //{
        //    var s = $"AuthenticationFailed: {arg.Exception.Message}";
        //    arg.Response.ContentLength = s.Length;
        //    arg.Response.Body.Write(Encoding.UTF8.GetBytes(s), 0, s.Length);
        //    return Task.FromResult(0);
        //}
    }
}
