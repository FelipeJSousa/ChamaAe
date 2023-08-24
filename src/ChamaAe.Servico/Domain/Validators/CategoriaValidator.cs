using ChamaAe.Servico.Domain.Entities;
using FluentValidation;

namespace ChamaAe.Servico.Domain.Validators;

public class CategoriaValidator : AbstractValidator<Categoria>
{
    public CategoriaValidator()
    {
        RuleFor(x => x.Nome).MinimumLength(3).WithMessage("O nome da categoria precisa ter ao menos 3 caracteres.");
        RuleFor(x => x.Nome).MaximumLength(50).WithMessage("O nome da categoria precisa ter no máximo 50 caracteres.");
        
        RuleFor(x => x.Descricao).MinimumLength(10).WithMessage("O nome da categoria precisa ter ao menos 10 caracteres.");
        RuleFor(x => x.Descricao).MaximumLength(150).WithMessage("O nome da categoria precisa ter no máximo 150 caracteres.");
        
    }
}