using DatabaseHelper.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DatabaseHelper.DependencyInjection
{
    public static class DatabaseHelperMiddleware
    {
        public static IServiceCollection AddDatabaseHelper(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IStoreProcedureHelper, StoreProcedureHelper>(new Func<IServiceProvider,
                StoreProcedureHelper>(s => new StoreProcedureHelper(connectionString)));

            return services;
        }
    }
}
