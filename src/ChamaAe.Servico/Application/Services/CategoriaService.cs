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

public class CategoriaService : ServiceBase<Categoria>, ICategoriaService
{
    public CategoriaService(IServiceProvider serviceProvider, IRepository<Categoria> repoBase) : base(serviceProvider, repoBase)
    {
    }

    public async Task<Categoria?> Deletar(Categoria obj)
    {
        var ret = await GetSingle(x => x.Id == obj.Id);

        if (ret is not null) return await Delete(ret, false);
        
        NewNotification("Categoria", "Não foi possível encontrar a categoria com o id " + obj.Id + ".");
        return null;
    }

    public async Task<Categoria?> Alterar(Categoria obj)
    {
        var ret = await GetSingle(x => x.Id == obj.Id);

        if (ret is not null) return await Save(obj);
        
        NewNotification("Categoria", "Não foi possível encontrar a categoria com o id " + obj.Id + ".");
        return null;
    }

    public async Task<Categoria?> Salvar(Categoria obj)
    {
        if (obj is not null) return await Save(obj);
        
        NewNotification("Categoria", "Não é possivel salvar um objeto nulo.");
        return null;
    }

    public async Task<IEnumerable<Categoria>> ListarTodos(Categoria? filter = null,
        Func<IQueryable<Categoria>, IIncludableQueryable<Categoria, object>>? include = null)
    {
        if(filter is null)
            return await GetList(include: include);

        Expression<Func<Categoria, bool>> predicate = PredicateBuilder.New<Categoria>(true);

        if (filter.Id > 0)
            predicate = predicate.And(x => x.Id == filter.Id);
        
        if (!string.IsNullOrEmpty(filter.Nome))
            predicate = predicate.And(x => x.Nome == filter.Nome);
        
        if (!string.IsNullOrEmpty(filter.Descricao))
            predicate = predicate.And(x => x.Descricao == filter.Descricao);
        
        return await GetList(predicate, include);
    }

    public async Task<Categoria?> ObterPorId(long id,
        Func<IQueryable<Categoria>, IIncludableQueryable<Categoria, object>>? include = null)
    {
        return await GetSingle(x => x.Id == id, include: include);
    }
}