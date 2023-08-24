using ChamaAe.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChamaAe.Servico.Infra.Data.Mappings;

public class UsuarioTipoMap : EntityMapBase<UsuarioTipo>
{
    public override void Configure(EntityTypeBuilder<UsuarioTipo> builder)
    {
        base.Configure(builder);

        builder.ToTable("TipoUsuario");
    }
}