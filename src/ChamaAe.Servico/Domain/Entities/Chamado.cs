using System;
using System.Linq.Expressions;
using LinqKit;

namespace ChamaAe.Servico.Domain.Entities;

public class Chamado : EntityBase
{
    public string Titulo { get; set; }
    
    public string Descricao { get; set; }
    
    public DateTime DataCriacao { get; set; }
    
    public DateTime? DataEncerramento { get; set; }

    public string? Solucao { get; set; }
    
    public SituacaoChamado Situacao { get; set; }

    #region ForeignKeys Properties

    public long? UsuarioSolicitante { get; set; }
    
    public long? UsuarioResponsavel { get; set; }
    
    public long? Categoria { get; set; }
    
    #endregion
    
    #region ForeignKeys Properties

    public Usuario? UsuarioSolicitanteObj { get; set; }
    
    public Usuario? UsuarioResponsavelObj { get; set; }
    
    public Categoria? CategoriaObj { get; set; }
    
    #endregion

    #region Regras de Negócio

    public Expression<Func<Chamado, bool>> getPredicate()
    {
        Expression<Func<Chamado, bool>> predicate = PredicateBuilder.New<Chamado>(true);
        
        if (Id > 0)
            predicate = predicate.And(x => x.Id == Id);
        
        if (!string.IsNullOrEmpty(Titulo))
            predicate = predicate.And(x => x.Titulo == Titulo);
        
        if (!string.IsNullOrEmpty(Descricao))
            predicate = predicate.And(x => x.Descricao == Descricao);
        
        if (!string.IsNullOrEmpty(Solucao))
            predicate = predicate.And(x => x.Solucao == Solucao);
        
        if (Categoria > 0)
            predicate = predicate.And(x => x.Categoria == Categoria);
        
        if (DataCriacao != DateTime.MinValue)
            predicate = predicate.And(x => x.DataCriacao == DataCriacao);
        
        if (DataEncerramento != null && this?.DataEncerramento != DateTime.MinValue)
            predicate = predicate.And(x => x.DataEncerramento == DataEncerramento);
        
        if (Situacao > 0)
            predicate = predicate.And(x => x.Situacao == Situacao);
        
        if (UsuarioSolicitante > 0)
            predicate = predicate.And(x => x.UsuarioSolicitante == UsuarioSolicitante);
        
        if (UsuarioResponsavel > 0)
            predicate = predicate.And(x => x.UsuarioResponsavel == UsuarioResponsavel);

        return predicate;
    }

    #endregion
    
}