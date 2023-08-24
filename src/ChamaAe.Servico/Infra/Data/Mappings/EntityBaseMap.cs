using ChamaAe.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChamaAe.Servico.Infra.Data.Mappings
{
    public abstract class EntityMapBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Ignore(x => x.ValidationResult);
        }
    }
}