using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ChamaAe.Servico.Application.ViewModels.Response;

[DisplayName("Chamado")]
[DataContract(Name = "ChamadoResponse", Namespace = "")]
public class ChamadoResponse : ViewModelBase
{
    [DataMember(Name = "Id")]
    public string? Id { get; set; } 
    
    [DataMember(Name = "Titulo")]
    public string? Titulo { get; set; }
    
    [DataMember(Name = "Descricao")]
    public string? Descricao { get; set; }
    
    [DataMember(Name = "Solucao")]
    public string? Solucao { get; set; }
    
    [DataMember(Name = "DataCriacao")]
    public DateTime? DataCriacao { get; set; }
    
    [DataMember(Name = "DataAlteracao")]
    public DateTime? DataAlteracao { get; set; }
    
    [DataMember(Name = "DataEncerramento")]
    public DateTime? DataEncerramento { get; set; }
    
    [DataMember(Name = "UsuarioSolicitante")]
    public long? UsuarioSolicitante { get; set; }
    
    [DataMember(Name = "UsuarioResponsavel")]
    public long? UsuarioResponsavel { get; set; }
    
    [DataMember(Name = "Categoria")]
    public long? Categoria { get; set; }
    
    [DataMember(Name = "CategoriaObj")]
    public CategoriaResponse? CategoriaObj { get; set; }
    
    [DataMember(Name = "Situacao")]
    public ChamadoSituacaoResponse? Situacao { get; set; }
    
    [DataMember(Name = "UsuarioSolicitanteObj")]
    public UsuarioResponse? UsuarioSolicitanteObj { get; set; }
    
    [DataMember(Name = "UsuarioResponsavelObj")]
    public UsuarioResponse? UsuarioResponsavelObj { get; set; }
}


[DisplayName("ChamadoSituacao")]
[DataContract(Name = "ChamadoSituacaoResponse", Namespace = "")]
public class ChamadoSituacaoResponse : ViewModelBase
{
    [DataMember(Name = "Id")]
    public int? Id { get; set; } 
    
    [DataMember(Name = "Nome")]
    public string? Nome { get; set; }
}