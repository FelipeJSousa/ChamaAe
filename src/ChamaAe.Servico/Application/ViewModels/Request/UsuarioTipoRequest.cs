using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ChamaAe.Servico.Application.ViewModels.Request;

[DisplayName("UsuarioTipo")]
[DataContract(Name = "UsuarioTipoRequest", Namespace = "")]
public class UsuarioTipoRequest : ViewModelBase
{
    [FromQuery(Name = "Id")]
    public long? Id { get; set; }
    
    [FromQuery(Name = "Nome")]
    public string? Nome { get; set; }
    
    [FromQuery(Name = "Descricao")]
    public string? Descricao { get; set; }
}

[DisplayName("UsuarioTipo")]
[DataContract(Name = "UsuarioTipoCreateRequest", Namespace = "")]
public class UsuarioTipoCreateRequest : ViewModelBase
{
    [FromBody]
    [DataMember(Name = "Nome")]
    public string Nome { get; set; }
    
    [FromBody]
    [DataMember(Name = "Descricao")]
    public string Descricao { get; set; }
}

[DisplayName("UsuarioTipo")]
[DataContract(Name = "UsuarioTipoAlterRequest", Namespace = "")]
public class UsuarioTipoAlterRequest : ViewModelBase
{
    [Required(ErrorMessage = "Id do UsuarioTipo é obrigatório.")]
    [FromBody]
    [DataMember(Name = "Id")]
    public long Id { get; set; }
    
    [FromBody]
    [DataMember(Name = "Nome")]
    public string? Nome { get; set; }
    
    [FromBody]
    [DataMember(Name = "Descricao")]
    public string? Descricao { get; set; }
}

[DisplayName("UsuarioTipo")]
[DataContract(Name = "UsuarioTipoAlterRequest", Namespace = "")]
public class UsuarioTipoDeleteRequest : ViewModelBase
{
    [FromRoute]
    [DataMember(Name = "Id")]
    [Required(ErrorMessage = "Id do UsuarioTipo é obrigatório.")]
    public long Id { get; set; }
}