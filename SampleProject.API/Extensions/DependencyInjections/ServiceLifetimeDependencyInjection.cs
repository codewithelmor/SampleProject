using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.BusinessLogicLayer.MappingProfiles;
using SampleProject.DataAccessLayer.Repositories;
using SampleProject.DataAccessLayer.UnitOfWorks;

namespace SampleProject.API.Extensions.DependencyInjections
{
    public static class ServiceLifetimeDependencyInjection
    {
        public static void AddServiceLifetime(this IServiceCollection services)
        {
            #region Auto Mapper

            services.AddSingleton(
                new MapperConfiguration(configure =>
                {
                    configure.AddProfile(new PersonMappingProfile());
                }).CreateMapper());

            #endregion

            #region Unit Of Work

            services.AddTransient<IApplicationUnitOfWork, ApplicationUnitOfWork>();

            #endregion

            #region Repositories

            services.AddTransient<IPersonRepository, PersonRepository>();

            #endregion

            #region Services


            #endregion
        }
    }
}
