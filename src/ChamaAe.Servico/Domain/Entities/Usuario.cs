using System;
using ChamaAe.Servico.Domain.Validators;

namespace ChamaAe.Servico.Domain.Entities;

public class Usuario : EntityBase
{
    public string Email { get; set; }
    
    public string Senha { get; set; }
    
    public string Nome { get; set; }
    
    public DateTime? DataCriacao { get; set; }
    
    public DateTime? DataAlteracao { get; set; }
    
    public string Cpf { get; set; }
    
    public string Endereco { get; set; }
    
    public string EnderecoNumero { get; set; }
    
    public string EnderecoBairro { get; set; }
    
    public string? EnderecoReferencia { get; set; }
    
    public string EnderecoCidade { get; set; }
    
    public string EnderecoEstado { get; set; }
    
    public string EnderecoCep { get; set; }
    

    #region ForeignKeys Properties

    public long? UsuarioTipo { get; set; }

    #endregion

    #region Navigation Properties

    public UsuarioTipo? UsuarioTipoObj { get; set; }

    #endregion

    #region Regras de negócio

    public override bool EhValido() => Validate(this, new UsuarioValidator());
    
    #endregion

}