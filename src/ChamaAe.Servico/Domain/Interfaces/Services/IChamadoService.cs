using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChamaAe.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace ChamaAe.Servico.Domain.Interfaces.Services;

public interface IChamadoService : IService
{
    Task<Chamado?> Assumir(long idChamado, long idResponsavel);

    Task<Chamado?> Deletar(Chamado obj);
    
    Task<Chamado?> Encerrar(long idChamado, string solucao);
    
    Task<Chamado?> Alterar(Chamado obj);

    Task<Chamado?> NovoChamado(Chamado obj);

    Task<IEnumerable<Chamado>> ListarTodos(Chamado? filter = null,
        Func<IQueryable<Chamado>, IIncludableQueryable<Chamado, object>>? include = null);

    Task<IEnumerable<Chamado>> ListarDetalhado(Expression<Func<Chamado, bool>>? predicate = null);
    
    Task<Chamado?> ObterPorId(long id,
        Func<IQueryable<Chamado>, IIncludableQueryable<Chamado, object>>? include = null);
}