using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SampleProject.API.Extensions.DependencyInjections
{
    public static class MediatorDependencyInjection
    {
        public static void AddMediator(this IServiceCollection services)
        {
            #region Person

            // Queries
            services.AddMediatR(typeof(SampleProject.BusinessLogicLayer.Mediators.PersonMediator.Queries.List.Handler).GetTypeInfo().Assembly);
            // Commands
            services.AddMediatR(typeof(SampleProject.BusinessLogicLayer.Mediators.PersonMediator.Commands.Add.Handler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(SampleProject.BusinessLogicLayer.Mediators.PersonMediator.Commands.Update.Handler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(SampleProject.BusinessLogicLayer.Mediators.PersonMediator.Commands.Toggle.Handler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(SampleProject.BusinessLogicLayer.Mediators.PersonMediator.Commands.Delete.Handler).GetTypeInfo().Assembly);

            #endregion
        }
    }
}
