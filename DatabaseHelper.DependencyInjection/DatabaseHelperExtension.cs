using DatabaseHelper.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.SqlClient;

namespace DatabaseHelper.DependencyInjection
{
    public static class DatabaseHelperExtension
    {
        public static IServiceCollection AddDatabaseHelper(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IStoreProcedureHelper>(x => new StoreProcedureHelper(connectionString));
            services.AddScoped<ICommandHelper>(x => new CommandHelper(connectionString));
            services.AddScoped<ICommandTransactionHelper>(x => new CommandTransactionHelper(new SqlConnection(connectionString)));

            return services;
        }

        public static IServiceCollection AddDatabaseHelper(this IServiceCollection services)
        {
            services.AddScoped<IStoreProcedureHelper>(x => new StoreProcedureHelper());
            services.AddScoped<ICommandHelper>(x => new CommandHelper());
			services.AddScoped<ICommandTransactionHelper>(x => new CommandTransactionHelper());

			return services;
        }
    }
}
