using ChamaAe.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChamaAe.Servico.Infra.Data.Mappings;

public class CategoriaMap : EntityMapBase<Categoria>
{
    public override void Configure(EntityTypeBuilder<Categoria> builder)
    {
        base.Configure(builder);

        builder.ToTable("Categoria");
    }
}