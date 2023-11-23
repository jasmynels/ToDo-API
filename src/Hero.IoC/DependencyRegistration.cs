using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.Quartz;
using Core.Sercurity;
using Infra.Data;
using IoC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace IoC
{
    public static class DependencyRegistration
    {
        public static void Register(Assembly assembly, IConfiguration configuration, IHostBuilder host, IServiceCollection services)
        {
            var defaultConnection = Environment.GetEnvironmentVariable("DefaultConnection");

            if (string.IsNullOrEmpty(defaultConnection))
                defaultConnection = configuration.GetConnectionString("DefaultConnection");

            Log.Logger = new LoggerConfiguration()
                //TODO: Pegar a URI via Environment...
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = configuration["ElasticConfiguration:IndexFormat"]
                })
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            host.UseSerilog();

            services.AddMediatR(assembly);
            services.AddMapping();

            services.AddEntityConfiguration(defaultConnection);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo API", Version = "v1" });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "seu_issuer",
                    ValidAudience = "seu_audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("teste123")),
                    ClockSkew = TimeSpan.Zero
                };
            });

            BuildDependencyInjectionProvider(assembly, host);
        }

        private static void BuildDependencyInjectionProvider(Assembly assembly, IHostBuilder host)
        {
            var coreAssembly = Assembly.GetAssembly(typeof(AuthenticatedUser));
            var infraAssembly = Assembly.GetAssembly(typeof(AppDbContext));

            host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            host.ConfigureContainer<ContainerBuilder>((_, builder) =>
            {
                if (assembly.GetName().Name == "Jobs")
                {
                    builder.RegisterModule(new QuartzAutofacFactoryModule());
                    builder.RegisterModule(new QuartzAutofacJobsModule(assembly));
                }

                builder.RegisterAssemblyTypes(assembly, coreAssembly, infraAssembly).AsImplementedInterfaces();
            });
        }
    }
}