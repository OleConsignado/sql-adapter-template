using AutoMapper;
using SqlAdapter.Template.TmdbAdapter.Clients;
using SqlAdapter.Template.Domain;

namespace SqlAdapter.Template.TmdbAdapter
{
    public class TmdbMapperProfile : Profile
    {
        public TmdbMapperProfile()
        {
            CreateMap<TmdbMoviesDto, Filme>()
                // Mapeia a propriedade TmdbMovieResult.Overview para Filme.Descricao.
                .ForMember(destino => destino.Descricao, opt => opt.MapFrom(origem => origem.Overview))
                // TmdbMovieResult.Title -> Filme.Nome
                .ForMember(destino => destino.Nome, opt => opt.MapFrom(origem => origem.Title))
                // TmdbMovieResult.ReleaseDate -> Filme.DataLancamento
                .ForMember(destino => destino.DataLancamento, opt => opt.MapFrom(origem => origem.ReleaseDate));

        }
    }
}
