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

            services.AddScoped<ICommandHelper, CommandHelper>(new Func<IServiceProvider,
                CommandHelper>(s => new CommandHelper(connectionString)));

            return services;
        }

        public static IServiceCollection AddDatabaseHelper(this IServiceCollection services)
        {
            services.AddScoped<IStoreProcedureHelper, StoreProcedureHelper>(new Func<IServiceProvider,
                StoreProcedureHelper>(s => new StoreProcedureHelper()));

            services.AddScoped<ICommandHelper, CommandHelper>(new Func<IServiceProvider,
                CommandHelper>(s => new CommandHelper()));

            return services;
        }
    }
}
