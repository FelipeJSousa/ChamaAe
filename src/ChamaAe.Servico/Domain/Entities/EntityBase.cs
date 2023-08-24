using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using ChamaAe.Servico.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace ChamaAe.Servico.Domain.Entities;

public abstract class EntityBase : IModelValidator
{
    public long Id { get; set; }

    #region Validation
    public virtual bool EhValido() => true;

    public virtual bool EhValidoAlterar() => EhValido();

    public virtual bool EhValidoRemover() => EhValido();

    [NotMapped]
    [IgnoreDataMember]
    public ValidationResult ValidationResult { get; set; }
    
    public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        ValidationResult = validator.Validate(model);
        return ValidationResult.IsValid;
    }
    
    #endregion
}
