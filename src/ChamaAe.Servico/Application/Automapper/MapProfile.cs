using AutoMapper;
using AutoMapper.Internal;
using ChamaAe.Servico.Application.ViewModels.Request;
using ChamaAe.Servico.Application.ViewModels.Response;
using ChamaAe.Servico.Domain.Entities;
using ChamaAe.Servico.Domain.Extensions;
using Microsoft.OpenApi.Extensions;

namespace ChamaAe.Servico.Application.Automapper;

public class MapProfile : Profile
{
    public MapProfile() : base("AutoMapperProfile")
    {
        AllowNullCollections = true;
        ((IProfileExpressionInternal) this).ForAllMaps((_, cnfg) => cnfg.ForAllMembers(opts => { opts.IgnoreSourceWhenDefault(); }));
            
        CreateMapRequest();
            
        CreateMapResponse();
            
    }

    private void CreateMapRequest()
    {
        CreateMap<CategoriaRequest, Categoria>().ReverseMap();
        CreateMap<CategoriaAlterRequest, Categoria>().ReverseMap();
        CreateMap<CategoriaCreateRequest, Categoria>().ReverseMap();
        CreateMap<CategoriaDeleteRequest, Categoria>().ReverseMap();
        
        CreateMap<UsuarioTipoRequest, UsuarioTipo>().ReverseMap();
        CreateMap<UsuarioTipoAlterRequest, UsuarioTipo>().ReverseMap();
        CreateMap<UsuarioTipoCreateRequest, UsuarioTipo>().ReverseMap();
        CreateMap<UsuarioTipoDeleteRequest, UsuarioTipo>().ReverseMap();
        
        CreateMap<UsuarioRequest, Usuario>().ReverseMap();
        CreateMap<UsuarioAlterRequest, Usuario>().ReverseMap();
        CreateMap<UsuarioCreateRequest, Usuario>().ReverseMap();
        CreateMap<UsuarioDeleteRequest, Usuario>().ReverseMap();
        
        CreateMap<LoginRequest, Usuario>().ReverseMap();
        
        CreateMap<ChamadoRequest, Chamado>().ReverseMap();
        CreateMap<ChamadoAlterRequest, Chamado>().ReverseMap();
        CreateMap<ChamadoCreateRequest, Chamado>().ReverseMap();
        CreateMap<ChamadoDeleteRequest, Chamado>().ReverseMap();
        CreateMap<ChamadoEncerrarRequest, Chamado>().ReverseMap();
        
    }

    private void CreateMapResponse()
    {
        CreateMap<CategoriaResponse, Categoria>().ReverseMap();
        CreateMap<UsuarioTipoResponse, UsuarioTipo>().ReverseMap();
        CreateMap<UsuarioResponse, Usuario>()
            .ReverseMap()
            .ForMember(dest => dest.UsuarioTipo, opt => opt.MapFrom(src => src.UsuarioTipoObj == null ? src.UsuarioTipo : null));
        CreateMap<LoginResponse, Usuario>()
            .ReverseMap()
            .ForMember(dest => dest.UsuarioObj, opt => opt.MapFrom(src => src))
            .AfterMap((src, dest) => dest.Sucesso = dest.UsuarioObj is not null);
        CreateMap<ChamadoResponse, Chamado>()
            .ReverseMap()
            .ForMember(dest => dest.UsuarioSolicitante,
                opt => opt.MapFrom(src => src.UsuarioSolicitanteObj == null ? src.UsuarioSolicitante : null))
            .ForMember(dest => dest.UsuarioResponsavel,
                opt => opt.MapFrom(src => src.UsuarioResponsavelObj == null ? src.UsuarioResponsavel : null))
            .ForMember(dest => dest.Categoria,
                opt => opt.MapFrom(src => src.CategoriaObj == null ? src.Categoria : null));
        CreateMap<ChamadoSituacaoResponse, SituacaoChamado>()
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.GetDescription()));
    }
}