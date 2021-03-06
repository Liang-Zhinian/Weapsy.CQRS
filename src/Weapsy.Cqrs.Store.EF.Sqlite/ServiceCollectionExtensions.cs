﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weapsy.Cqrs.Store.EF.Extensions;

namespace Weapsy.Cqrs.Store.EF.Sqlite
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeapsyCqrsSqliteProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWeapsyCqrsEFProvider(configuration);

            var connectionString = configuration.GetSection(Constants.DomainDbConfigurationConnectionString).Value;

            services.AddDbContext<DomainDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddTransient<IDatabaseProvider, SqliteDatabaseProvider>();

            return services;
        }
    }
}
