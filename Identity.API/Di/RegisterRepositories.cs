using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Identity.API.Di
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var dataAssembly = Assembly.Load("Identity.Infrastructure");

            dataAssembly.GetTypesForPath("Identity.Infrastructure.Repositories")
                .ForEach(p =>
                {
                    var interfaceValue = p.GetInterfaces().FirstOrDefault();

                    if (interfaceValue != null)
                    {
                        services.AddTransient(interfaceValue.UnderlyingSystemType, p.UnderlyingSystemType);
                    }
                });
            return services;
        }
    }
}
