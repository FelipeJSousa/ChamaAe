using ChamaAe.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChamaAe.Servico.Infra.Data.Mappings;

public class UsuarioMap : EntityMapBase<Usuario>
{
    public override void Configure(EntityTypeBuilder<Usuario> builder)
    {
        base.Configure(builder);

        builder.ToTable("Usuario");

        builder.Property(x => x.UsuarioTipo).HasColumnName("TipoUsuario");
        
        builder.HasOne(x => x.UsuarioTipoObj).WithMany().HasForeignKey(x => x.UsuarioTipo).IsRequired().OnDelete(DeleteBehavior.ClientSetNull);
        
    }
}