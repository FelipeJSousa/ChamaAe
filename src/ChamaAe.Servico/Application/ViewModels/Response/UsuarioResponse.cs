using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ChamaAe.Servico.Application.ViewModels.Response;

[DisplayName("Usuario")]
[DataContract(Name = "UsuarioResponse", Namespace = "")]
public class UsuarioResponse : ViewModelBase
{
    [DataMember(Name = "Id")]
    public long Id { get; set; }
    
    [DataMember(Name = "Nome")]
    public string? Nome { get; set; }
    
    [DataMember(Name = "Email")]
    public string? Email { get; set; }
    
    [DataMember(Name = "UsuarioTipo")]
    public long? UsuarioTipo { get; set; }
    
    [DataMember(Name = "DataCriacao")]
    public DateTime? DataCriacao { get; set; }
    
    [DataMember(Name = "DataAlteracao")]
    public DateTime? DataAlteracao { get; set; }

    [DataMember(Name = "UsuarioTipoObj")] 
    public UsuarioTipoResponse? UsuarioTipoObj { get; set; }
    
    [DataMember(Name = "Cpf")]
    public string Cpf { get; set; }
    
    [DataMember(Name = "Endereco")]
    public string Endereco { get; set; }
    
    [DataMember(Name = "EnderecoNumero")]
    public string EnderecoNumero { get; set; }
    
    [DataMember(Name = "EnderecoBairro")]
    public string EnderecoBairro { get; set; }
    
    [DataMember(Name = "EnderecoReferencia")]
    public string? EnderecoReferencia { get; set; }
    
    [DataMember(Name = "EnderecoCidade")]
    public string EnderecoCidade { get; set; }
    
    [DataMember(Name = "EnderecoEstado")]
    public string EnderecoEstado { get; set; }
    
    [DataMember(Name = "EnderecoCep")]
    public string EnderecoCep { get; set; }
}