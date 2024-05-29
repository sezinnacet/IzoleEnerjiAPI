using Business.Abstract;
using Business.Concrete;
using Core.Extensions;
using DataAccess.Abstract;
using DataAccess.Concrete;
using IzoleEnerjiAPI.Middleware;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Globalization;

namespace IzoleEnerjiAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            using var client = new IzoleEnerjiDbContext();
            var connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            IzoleEnerjiDbContext.SetConnectionString(connectionString);

            client.Database.EnsureCreated();
            client.SeedInitialData();
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //var oktaAuthority = Configuration["OktaConfig:Authority"];
            //var oktaAudience = Configuration["OktaConfig:Audience"];
            services.AddControllers();


            #region AddSingleton
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUserService, UserManager>();
            services.AddSingleton<IUserDal, EfUserDal>();
            services.AddSingleton<IUserDetailDal, EfUserDetailDal>();
            services.AddSingleton<IPremiumModeDal, EfPremiumModeDal>();
            services.AddSingleton<IProductDal, EfProductDal>();
            services.AddSingleton<IProductService, ProductManager>();
            #endregion AddSingleton
            #region registerService
            services.AddDbContext<IzoleEnerjiDbContext>();
            #endregion


            /*  #region hangfire 
              services.AddHangfire(config =>
              {
                  config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                      .UseSimpleAssemblyNameTypeSerializer()
                      .UseRecommendedSerializerSettings();
                  config.UseSqlServerStorage(() => new SqlConnection(this.Configuration.GetConnectionString("MTTContext")), new SqlServerStorageOptions
                  {
                      PrepareSchemaIfNecessary = true,
                      QueuePollInterval = TimeSpan.FromMinutes(5),
                      CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                      SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                      UseRecommendedIsolationLevel = true,
                      UsePageLocksOnDequeue = true,
                      DisableGlobalLocks = true
                  })
                  .WithJobExpirationTimeout(TimeSpan.FromHours(6));
              }).AddHangfireServer(_ => new BackgroundJobServerOptions
              {

                  SchedulePollingInterval = TimeSpan.FromSeconds(30),// 30 saniyede bir çalışacak job var mı kontrol et
                  WorkerCount = Environment.ProcessorCount * 5 //arka planda çalışacak job sayısı
              });
              #endregion
            */

            //services.AddHostedService<WorkerService>();
            //services.AddSignalR(options =>
            //{
            //    options.EnableDetailedErrors = true;
            //    options.HandshakeTimeout = TimeSpan.FromMinutes(1);
            //});
            #region localization
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                CultureInfo[] cultures = new CultureInfo[]
                {
                     new("tr-TR"),
                     new("en-US"),
                     new("fr-FR"),
                     new("ru-RU"),
                     new("ro-RO")
                };
                options.DefaultRequestCulture = new RequestCulture("tr-TR");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });
            #endregion
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.
                        WithOrigins("*").
                        WithMethods("GET", "POST", "GET, POST, PUT, DELETE, OPTIONS").
                        AllowAnyOrigin().
                        WithHeaders(HeaderNames.ContentType));
            });

            /*   #region BasicAuthentication
               BasicAuthenticationService basicAuthentication = new(Configuration.GetSection("BasicAuthUsers").Get<List<BasicAuthUser>>());

               services
                   .AddAuthentication("BasicAuthentication")
                   .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
               services
                   .AddScoped<IBasicAuthenticationService>(provider => basicAuthentication);
               #endregion

               services.AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(options =>
               {
                   options.Authority = oktaAuthority;
                   options.Audience = oktaAudience;
               });

               */
            /*Swagger Configurations*/
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("public", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MTT.WebApi Public",
                    Description = "MTT WebApi Public Endpoints Swagger Surface"
                });
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MTT.WebApi",
                    Description = "MTT WebApi Swagger Surface"
                });
            });

            AutoMapperExtension.ConfigureMapping(services);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /*   #region hangfire-backgroundjob
               app.UseHangfireDashboard("/hangfire", new DashboardOptions
               {
                   DashboardTitle = "MTT Hangfire DashBoard",
               });

               var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
               if (scopeFactory != null)
                   GlobalConfiguration.Configuration.UseActivator(new AspNetCoreJobActivator(scopeFactory));
               GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 3 });

               var jFactory = app.ApplicationServices.GetRequiredService<IRecurringJobs>();
               if (jFactory != null)
                   jFactory.RunJobs();

               #endregion */
            app.UseFileServer();
            app.UseHttpsRedirection();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<OptionsMiddleware>();
            //app.UseMiddleware<LocalizationMessageMiddleware>();
            app.UseCors(options => options.AllowAnyOrigin());
            //var hangfireConfigurations = configuration.GetSection(nameof(HangfireConfigurations)).Get<HangfireConfigurations>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            //app.UseHangfireDashboard("/hangfireAdmin", new DashboardOptions()
            //{
            //    Authorization = new[]
            //    { new HangfireCustomBasicAuthenticationFilter { Pass = hangfireConfigurations.Password, User = hangfireConfigurations.User } }
            //});
            // Enable Swagger UI for the public documentation
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("swagger.json", "Public API V1");
            //    c.RoutePrefix = "swagger/public"; // Set the Swagger UI route prefix
            //});
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("v1/swagger.json", "MTT API V1");
            });
        }
    }
}
