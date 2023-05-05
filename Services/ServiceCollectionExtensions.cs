using CompetitionEventsManager.Repository.IRepository;
using CompetitionEventsManager.Repository;
using Microsoft.AspNetCore.JsonPatch.Internal;
using CompetitionEventsManager.Services.Adapters;
using CompetitionEventsManager.Services.Adapters.IAdapters;
using CompetitionEventsManager.Services.IServices;

namespace CompetitionEventsManager.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPacketServices(this IServiceCollection services)
        {

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPizzaRepository, PizzaRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddTransient<IPizzaAdapter, PizzaAdapter>();
            services.AddTransient<IOrderAdapter, OrderAdapter>();

            return services;
        }
    }
}

