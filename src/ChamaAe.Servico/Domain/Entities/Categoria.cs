using ChamaAe.Servico.Domain.Validators;

namespace ChamaAe.Servico.Domain.Entities;

public class Categoria : EntityBase
{
    public string Nome { get; set; }
    
    public string Descricao { get; set; }

    public override bool EhValido() => Validate(this, new CategoriaValidator());
}