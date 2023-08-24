using ChamaAe.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChamaAe.Servico.Infra.Data.Mappings;

public class ChamadoMap : EntityMapBase<Chamado>
{
    public override void Configure(EntityTypeBuilder<Chamado> builder)
    {
        base.Configure(builder);

        builder.ToTable("Chamado");
        
        builder.HasOne(x => x.UsuarioSolicitanteObj).WithMany().HasForeignKey(x => x.UsuarioSolicitante).IsRequired().OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.UsuarioResponsavelObj).WithMany().HasForeignKey(x => x.UsuarioResponsavel).IsRequired(false);
        builder.HasOne(x => x.CategoriaObj).WithMany().HasForeignKey(x => x.Categoria).IsRequired().OnDelete(DeleteBehavior.ClientSetNull);
        
    }
}