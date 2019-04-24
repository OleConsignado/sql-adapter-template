using AutoMapper;
using Dapper;
using Otc.Caching.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlAdapter.Template.Domain;
using System.Data;
using SqlAdapter.Template.TmdbAdapter.Clients;

namespace SqlAdapter.Template.TmdbAdapter
{
    internal class TmdbRepository : ITmdbRepository
    {
        private readonly IDbConnection dbConnection;
        private readonly SqlAdapterConfiguration sqlAdapterConfiguration;
        private readonly ITypedCache typedCache;

        public TmdbRepository(IDbConnection dbConnection, 
            SqlAdapterConfiguration tmdbAdapterConfiguration, 
            ITypedCache typedCache)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            this.sqlAdapterConfiguration = tmdbAdapterConfiguration ?? throw new ArgumentNullException(nameof(tmdbAdapterConfiguration));
            this.typedCache = typedCache ?? throw new ArgumentNullException(nameof(typedCache));
        }

        public async Task<IEnumerable<Filme>> GetFilmesAsync(Pesquisa pesquisa)
        {
            var cacheKey = $"filmes::{pesquisa.TermoPesquisa}::{pesquisa.AnoLancamento}";

            if (!typedCache.TryGet(cacheKey, out IEnumerable<TmdbMoviesDto> tmdbMovies))
            {
                var query = @"SELECT Id, Title, Overview, ReleaseDate 
                            FROM Tmdb 
                            WHERE Title like @TermoPesquisa
                                AND ReleaseDate = @AnoLancamento";

                var parametros = new DynamicParameters();

                parametros.Add("TermoPesquisa", $"%{pesquisa.TermoPesquisa}%");
                parametros.Add("AnoLancamento", pesquisa.AnoLancamento);

                tmdbMovies = await dbConnection.QueryAsync<TmdbMoviesDto>(query);

                typedCache.Set(cacheKey, tmdbMovies, TimeSpan.FromSeconds(sqlAdapterConfiguration.SegundosValidadeCacheParametro));
            }

            var filmes = Mapper.Map<IEnumerable<Filme>>(tmdbMovies);

            return filmes;

        }
    }
}
