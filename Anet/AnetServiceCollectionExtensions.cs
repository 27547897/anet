﻿using Anet.Data;
using System;
using System.Data;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AnetServiceCollectionExtensions
    {
        /// <summary>
        /// Adds database services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TDb">The custom type of <see cref="Database"/>.</typeparam>
        /// <typeparam name="TDbConnection">The type of a db provider's connection.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="connectionString">The database connection string.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddDatabase<TDb, TDbConnection>(this IServiceCollection services, string connectionString)
            where TDb : Database
            where TDbConnection : IDbConnection, new()
        {
            return services.AddScoped((serviceProvider) =>
            {
                var connection = new TDbConnection() as IDbConnection;
                connection.ConnectionString = connectionString;

                var db = (TDb)Activator.CreateInstance(typeof(TDb), connection);
                return db;
            });
        }

        /// <summary>
        /// Adds database services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TDbConnection">The type of a db provider's connection.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="connectionString">The database connection string.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddDatabase<TDbConnection>(this IServiceCollection services, string connectionString)
            where TDbConnection : IDbConnection, new()
        {
            return AddDatabase<Database,TDbConnection>(services, connectionString);
        }
    }
}
