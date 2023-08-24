using AutoMapper;
using AutoMapper.Internal;
using ChamaAe.Servico.Domain.Entities;

namespace ChamaAe.Servico.Application.Automapper;

public class AutoMapperProfile : Profile                    // Self Reference para utilizar no merge do alterar entity framework
{
    public AutoMapperProfile() : base("AutoMapperProfile")
    {
        AllowNullCollections = true;

        ((IProfileExpressionInternal) this).ForAllMaps((_, cnfg) => cnfg.ForAllMembers(opts => opts.IgnoreSourceAndDefault()));
        ((IProfileExpressionInternal) this).ForAllMaps((_, e) => e.AfterMap(AutoMapperConfig.SetNullFromNullableDefault));

        CreateMap<Categoria, Categoria>().ReverseMap();
        CreateMap<UsuarioTipo, UsuarioTipo>().ReverseMap();
        CreateMap<Usuario, Usuario>().ReverseMap();
        CreateMap<Chamado, Chamado>().ReverseMap();

    }
}