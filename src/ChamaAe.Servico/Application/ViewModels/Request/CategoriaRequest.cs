using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ChamaAe.Servico.Application.ViewModels.Request;

[DisplayName("Categoria")]
[DataContract(Name = "CategoriaRequest", Namespace = "")]
public class CategoriaRequest : ViewModelBase
{
    [FromQuery(Name = "Id")]
    public long? Id { get; set; }
    
    [FromQuery(Name = "Nome")]
    public string? Nome { get; set; }
    
    [FromQuery(Name = "Descricao")]
    public string? Descricao { get; set; }
}

[DisplayName("Categoria")]
[DataContract(Name = "CategoriaCreateRequest", Namespace = "")]
public class CategoriaCreateRequest : ViewModelBase
{
    [FromBody]
    [DataMember(Name = "Nome")]
    public string Nome { get; set; }
    
    [FromBody]
    [DataMember(Name = "Descricao")]
    public string Descricao { get; set; }
}

[DisplayName("Categoria")]
[DataContract(Name = "CategoriaAlterRequest", Namespace = "")]
public class CategoriaAlterRequest : ViewModelBase
{
    [Required(ErrorMessage = "Id da Categoria é obrigatório.")]
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

[DisplayName("Categoria")]
[DataContract(Name = "CategoriaAlterRequest", Namespace = "")]
public class CategoriaDeleteRequest : ViewModelBase
{
    [FromRoute]
    [DataMember(Name = "Id")]
    [Required(ErrorMessage = "Id da Categoria é obrigatório.")]
    public long Id { get; set; }
}