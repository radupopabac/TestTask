using Identity.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Di
{
    public static class UserDataProtection
    {
        public static IServiceCollection AddUserDataProtection(this IServiceCollection services)
        {

            var build = services.BuildServiceProvider();

            // need to do data migrations before we set DataProtection
            var scope = build.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();


            // update the data protection options in order to persist them in database
            services.AddDataProtection()
                .SetApplicationName("Identity")
                .AddKeyManagementOptions(options => options.XmlRepository = build.GetService<IXmlRepository>())
                // instead of this we could use a certificate if we add it to the solution
                // this is used to keep the key encrypted in the database
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });
            return services;
        }
    }
}
