﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weapsy.Cqrs.Store.EF.Extensions;

namespace Weapsy.Cqrs.Store.EF.PostgreSql
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeapsyCqrsPostgreSqlProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWeapsyCqrsEFProvider(configuration);

            var connectionString = configuration.GetSection(Constants.DomainDbConfigurationConnectionString).Value;

            services.AddDbContext<DomainDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddTransient<IDatabaseProvider, PostgreSqlDatabaseProvider>();

            return services;
        }
    }
}
