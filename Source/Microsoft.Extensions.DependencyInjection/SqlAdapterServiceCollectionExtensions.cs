using SqlAdapter.Template.Domain;
using SqlAdapter.Template.TmdbAdapter;
using SqlAdapter.Template.TmdbAdapter.Clients;
using Otc.Networking.Http.Client.Abstractions;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqlAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddTmdbAdapter(this IServiceCollection services, SqlAdapterConfiguration sqlAdapterConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (sqlAdapterConfiguration == null)
                throw new ArgumentNullException(nameof(sqlAdapterConfiguration));

            // Registra a instancia do objeto de configuracoes desta camanda.
            services.AddSingleton(sqlAdapterConfiguration);


            services.AddScoped<IDbConnection>(d =>
            {
                return new SqlConnection(sqlAdapterConfiguration.SqlConnectionString);
            });

            services.AddScoped<ITmdbRepository, TmdbRepository>();

            return services;
        }
    }
}
