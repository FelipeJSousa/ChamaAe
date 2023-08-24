using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChamaAe.Servico.Domain.Entities;
using ChamaAe.Servico.Domain.Interfaces;
using ChamaAe.Servico.Domain.Interfaces.Services;
using LinqKit;
using Microsoft.EntityFrameworkCore.Query;

namespace ChamaAe.Servico.Application.Services;

public class UsuarioTipoService : ServiceBase<UsuarioTipo>, IUsuarioTipoService
{
    public UsuarioTipoService(IServiceProvider serviceProvider, IRepository<UsuarioTipo> repoBase) : base(serviceProvider, repoBase)
    {
    }

    public async Task<UsuarioTipo?> Deletar(UsuarioTipo obj)
    {
        var ret = await GetSingle(x => x.Id == obj.Id);

        if (ret is not null) return await Delete(ret, false);
        
        NewNotification("UsuarioTipo", "Não foi possível encontrar o UsuarioTipo com o id " + obj.Id + ".");
        return null;
    }

    public async Task<UsuarioTipo?> Alterar(UsuarioTipo obj)
    {
        var ret = await GetSingle(x => x.Id == obj.Id);

        if (ret is not null) return await Save(obj);
        
        NewNotification("UsuarioTipo", "Não foi possível encontrar o UsuarioTipo com o id " + obj.Id + ".");
        return null;
    }

    public async Task<UsuarioTipo?> Salvar(UsuarioTipo obj)
    {
        if (obj is not null) return await Save(obj);
        
        NewNotification("UsuarioTipo", "Não é possivel salvar um objeto nulo.");
        return null;
    }

    public async Task<IEnumerable<UsuarioTipo>> ListarTodos(UsuarioTipo? filter = null,
        Func<IQueryable<UsuarioTipo>, IIncludableQueryable<UsuarioTipo, object>>? include = null)
    {
        if(filter is null)
            return await GetList(include: include);

        Expression<Func<UsuarioTipo, bool>> predicate = PredicateBuilder.New<UsuarioTipo>(true);

        if (filter.Id > 0)
            predicate = predicate.And(x => x.Id == filter.Id);
        
        if (!string.IsNullOrEmpty(filter.Nome))
            predicate = predicate.And(x => x.Nome == filter.Nome);
        
        if (!string.IsNullOrEmpty(filter.Descricao))
            predicate = predicate.And(x => x.Descricao == filter.Descricao);
        
        return await GetList(predicate, include);
    }

    public async Task<UsuarioTipo?> ObterPorId(long id,
        Func<IQueryable<UsuarioTipo>, IIncludableQueryable<UsuarioTipo, object>>? include = null)
    {
        return await GetSingle(x => x.Id == id, include: include);
    }
}