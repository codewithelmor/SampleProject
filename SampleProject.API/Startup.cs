using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleProject.API.Extensions.Builders;
using SampleProject.API.Extensions.DependencyInjections;
using SampleProject.DataAccessLayer.Application;
using Serilog;

namespace SampleProject.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Installation
            // https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-install
            // https://github.com/aspnet/EntityFrameworkCore/issues/15448
            // https://www.nuget.org/packages/dotnet-ef/
            // Commands
            // dotnet-ef migrations add InitialMigration -p ../SampleProject.DataAccessLayer -s ../SampleProject.API
            // dotnet-ef database update
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    connectionString: Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: builder => builder.MigrationsAssembly("SampleProject.DataAccessLayer")));

            services.AddMediator();

            services.AddServiceLifetime();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseCustomExceptionHandler(logger);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
