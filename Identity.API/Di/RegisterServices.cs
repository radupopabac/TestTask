using Identity.Core.Interfaces;
using Identity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Identity.API.Di
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var dataAssembly = Assembly.Load("Identity.Core");

            services.AddTransient<ITokenGeneration, TokenGeneration>();

            dataAssembly.GetTypesForPath("Identity.Core.Services")
                .ForEach(p =>
                {
                    var interfaceValue = p.GetInterfaces().FirstOrDefault();
                    services.AddTransient(interfaceValue.UnderlyingSystemType, p.UnderlyingSystemType);
                });

            return services;
        }
    }
}
