using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ChamaAe.Servico.Application.ViewModels.Request;

[DisplayName("Usuario")]
[DataContract(Name = "UsuarioRequest", Namespace = "")]
public class UsuarioRequest : ViewModelBase
{
    [FromQuery(Name = "Id")]
    public long? Id { get; set; }
    
    [FromQuery(Name = "Nome")]
    public string? Nome { get; set; }
    
    [FromQuery(Name = "Email")]
    public string? Email { get; set; }
    
    [FromQuery(Name = "UsuarioTipo")]
    public long? UsuarioTipo { get; set; }
    
    [FromQuery(Name = "Cpf")]
    public string Cpf { get; set; }
    
    [FromQuery(Name = "Endereco")]
    public string Endereco { get; set; }
    
    [FromQuery(Name = "EnderecoNumero")]
    public string EnderecoNumero { get; set; }
    
    [FromQuery(Name = "EnderecoBairro")]
    public string EnderecoBairro { get; set; }
    
    [FromQuery(Name = "EnderecoReferencia")]
    public string? EnderecoReferencia { get; set; }
    
    [FromQuery(Name = "EnderecoCidade")]
    public string? EnderecoCidade { get; set; }
    
    [FromQuery(Name = "EnderecoEstado")]
    public string? EnderecoEstado { get; set; }
    
    [FromQuery(Name = "EnderecoCep")]
    public string? EnderecoCep { get; set; }
}

[DisplayName("Usuario")]
[DataContract(Name = "UsuarioCreateRequest", Namespace = "")]
public class UsuarioCreateRequest : ViewModelBase
{
    [FromBody]
    [DataMember(Name = "Nome")]
    public string Nome { get; set; }
    
    [FromBody]
    [DataMember(Name = "Email")]
    public string? Email { get; set; }
    
    [FromBody]
    [DataMember(Name = "Senha")]
    public string Senha { get; set; }
    
    [FromBody]
    [DataMember(Name = "UsuarioTipo")]
    public long? UsuarioTipo { get; set; }
    
    [FromBody]
    [DataMember(Name = "Cpf")]
    public string Cpf { get; set; }
    
    [FromBody]
    [DataMember(Name = "Endereco")]
    public string Endereco { get; set; }
    
    [FromBody]
    [DataMember(Name = "EnderecoNumero")]
    public string EnderecoNumero { get; set; }
    
    [FromBody]
    [DataMember(Name = "EnderecoBairro")]
    public string EnderecoBairro { get; set; }
    
    [FromBody]
    [DataMember(Name = "EnderecoReferencia")]
    public string? EnderecoReferencia { get; set; }
    
    [FromBody]
    [DataMember(Name = "EnderecoCidade")]
    public string EnderecoCidade { get; set; }
    
    [FromBody]
    [DataMember(Name = "EnderecoEstado")]
    public string EnderecoEstado { get; set; }
    
    [FromBody]
    [DataMember(Name = "EnderecoCep")]
    public string EnderecoCep { get; set; }
    
}

[DisplayName("Usuario")]
[DataContract(Name = "UsuarioAlterRequest", Namespace = "")]
public class UsuarioAlterRequest : ViewModelBase
{
    [Required(ErrorMessage = "Id do Usuario é obrigatório.")]
    [FromBody]
    [DataMember(Name = "Id")]
    public long Id { get; set; }
    
    [FromBody]
    [DataMember(Name = "Nome")]
    public string? Nome { get; set; }
    
    [FromBody]
    [DataMember(Name = "Email")]
    public string? Email { get; set; }
    
    [FromBody]
    [DataMember(Name = "Senha")]
    public string Senha { get; set; }
    
    [FromBody]
    [DataMember(Name = "UsuarioTipo")]
    public long? UsuarioTipo { get; set; }
    
    [FromBody]
    [DataMember(Name = "Cpf")]
    public string Cpf { get; set; }
    
    [FromBody]
    [DataMember(Name = "Endereco")]
    public string Endereco { get; set; }
    
    [FromBody]
    [DataMember(Name = "EnderecoNumero")]
    public string EnderecoNumero { get; set; }
    
    [FromBody]
    [DataMember(Name = "EnderecoBairro")]
    public string EnderecoBairro { get; set; }
    
    [FromBody]
    [DataMember(Name = "EnderecoReferencia")]
    public string? EnderecoReferencia { get; set; }
    
    [FromBody]
    [DataMember(Name = "EnderecoCidade")]
    public string EnderecoCidade { get; set; }
    
    [FromBody]
    [DataMember(Name = "EnderecoEstado")]
    public string EnderecoEstado { get; set; }
    
    [FromBody]
    [DataMember(Name = "EnderecoCep")]
    public string EnderecoCep { get; set; }
}

[DisplayName("Usuario")]
[DataContract(Name = "UsuarioAlterRequest", Namespace = "")]
public class UsuarioDeleteRequest : ViewModelBase
{
    [FromRoute]
    [DataMember(Name = "Id")]
    [Required(ErrorMessage = "Id do Usuario é obrigatório.")]
    public long Id { get; set; }
}