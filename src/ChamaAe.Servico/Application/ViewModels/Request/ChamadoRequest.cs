using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ChamaAe.Servico.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChamaAe.Servico.Application.ViewModels.Request;

[DisplayName("Chamado")]
[DataContract(Name = "ChamadoRequest", Namespace = "")]
public class ChamadoRequest : ViewModelBase
{
    [FromQuery(Name = "Id")]
    public long? Id { get; set; }
    
    [FromQuery(Name = "Titulo")]
    public string? Titulo { get; set; }
    
    [FromQuery(Name = "Descricao")]
    public string? Descricao { get; set; }
    
    [FromQuery(Name = "Solucao")]
    public long? Solucao { get; set; }
    
    [FromQuery(Name = "DataCriacao")]
    public DateTime? DataCriacao { get; set; }
    
    [FromQuery(Name = "DataAlteracao")]
    public DateTime? DataAlteracao { get; set; }
    
    [FromQuery(Name = "DataEncerramento")]
    public DateTime? DataEncerramento { get; set; }
    
    [FromQuery(Name = "UsuarioSolicitante")]
    public long? UsuarioSolicitante { get; set; }
    
    [FromQuery(Name = "UsuarioResponsavel")]
    public long? UsuarioResponsavel { get; set; }
    
    [FromQuery(Name = "Categoria")]
    public long? Categoria { get; set; }

    [FromQuery(Name = "Situacao")]
    public SituacaoChamado Situacao { get; set; }
}

[DisplayName("Chamado")]
[DataContract(Name = "ChamadoCreateRequest", Namespace = "")]
public class ChamadoCreateRequest : ViewModelBase
{
    [FromBody]
    [DataMember(Name = "Titulo")]
    public string? Titulo { get; set; }

    [FromBody]
    [DataMember(Name = "Descricao")]
    public string? Descricao { get; set; }

    [FromBody]
    [DataMember(Name = "UsuarioSolicitante")]
    public long? UsuarioSolicitante { get; set; }

    [FromBody]
    [DataMember(Name = "Categoria")]
    public long? Categoria { get; set; }
}

[DisplayName("Chamado")]
[DataContract(Name = "ChamadoAlterRequest", Namespace = "")]
public class ChamadoAlterRequest : ViewModelBase
{
    [Required(ErrorMessage = "Id do Chamadoé obrigatório.")]
    [FromBody]
    [DataMember(Name = "Id")]
    public string? Id { get; set; }
    
    [DataMember(Name = "Titulo")]
    public string? Titulo { get; set; }
    
    [DataMember(Name = "Descricao")]
    public string? Descricao { get; set; }
    
    [DataMember(Name = "Solucao")]
    public long? Solucao { get; set; }
    
    [DataMember(Name = "Categoria")]
    public long? Categoria { get; set; }
    
}

[DisplayName("ChamadoDeleteEncerrar")]
[DataContract(Name = "ChamadoDeleteEncerrarRequest", Namespace = "")]
public class ChamadoDeleteRequest : ViewModelBase
{
    [FromRoute]
    [DataMember(Name = "Id")]
    [Required(ErrorMessage = "Id do Chamado é obrigatório.")]
    public long Id { get; set; }
}

[DisplayName("ChamadoEncerrar")]
[DataContract(Name = "ChamadoEncerrarRequest", Namespace = "")]
public class ChamadoEncerrarRequest : ViewModelBase
{
    [FromBody]
    [DataMember(Name = "Id")]
    [Required(ErrorMessage = "Id do Chamado é obrigatório.")]
    public long Id { get; set; }
    
    [FromBody]
    [DataMember(Name = "Solucao")]
    [Required(ErrorMessage = "A solução do Chamado é obrigatório.")]
    public string Solucao { get; set; }
}

[DisplayName("ChamadoAssumir")]
[DataContract(Name = "ChamadoAlterRequest")]
public class ChamadoAssumirRequest : ViewModelBase
{
    [Required(ErrorMessage = "Id do Chamado é obrigatório.")]
    [DataMember(Name = "IdChamado")]
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Id do Usuario responsável é obrigatório.")]
    [DataMember(Name = "IdUsuarioReponsavel")]
    public long UsuarioReponsavel { get; set; }
    
}