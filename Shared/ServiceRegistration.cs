using RCMS.DBHelpers;
using RCMS.EncryptDecryptHelpers;
using RCMS.Filters;
using RCMS.IRepositories;
using RCMS.IServices;
using RCMS.Repositories;
using RCMS.Services;

namespace RCMS.Shared
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            //services.AddScoped<IAESRepository, AESHelper>();
            services.AddScoped<ISqlDBConnectionRepository, SqlDBConnectionHelper>();
            services.AddScoped<IDBContextRepository, DBContext>();

            services.AddScoped<IRegistrationsRepositories, RegistrationsRepositories>();
            services.AddScoped<IRegistrationService, RegistrationService>();



            services.AddScoped<AESCryptoService>();
            services.AddScoped<HMACDecryptService>();
            services.AddScoped<RSADecryptService>();

            services.AddScoped<EncryptionActionFilter>();

            return services;
        }
    }
}
