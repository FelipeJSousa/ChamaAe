using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChamaAe.Servico.Domain.Entities;
using ChamaAe.Servico.Domain.Interfaces;
using ChamaAe.Servico.Domain.Interfaces.Services;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ChamaAe.Servico.Application.Services;

public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
{
    public UsuarioService(IServiceProvider serviceProvider, IRepository<Usuario> repoBase) : base(serviceProvider, repoBase)
    {
    }

    public async Task<Usuario?> Deletar(Usuario obj)
    {
        var ret = await GetSingle(x => x.Id == obj.Id);

        if (ret is not null) return await Delete(ret, false);
        
        NewNotification("Usuario", "Não foi possível encontrar o Usuario com o id " + obj.Id + ".");
        return null;
    }

    public async Task<Usuario?> Alterar(Usuario obj)
    {
        var ret = await GetSingle(x => x.Id == obj.Id);

        if (ret is not null)
        {
            if(!string.IsNullOrEmpty(obj.Cpf))
                obj.Cpf = obj.Cpf.Replace("-", "").Replace(".", "");
            obj.DataAlteracao = DateTime.Now;
            return await Save(obj);
        }
        
        NewNotification("Usuario", "Não foi possível encontrar o Usuario com o id " + obj.Id + ".");
        return null;
    }

    public async Task<Usuario?> Salvar(Usuario obj)
    {
        if (obj is not null)
        {
            obj.Cpf = obj.Cpf.Replace("-", "").Replace(".", "");
            obj.DataCriacao = DateTime.Now; 
            return await Save(obj);
        }
        
        NewNotification("Usuario", "Não é possivel salvar um objeto nulo.");
        return null;
    }

    public async Task<IEnumerable<Usuario>> ListarTodos(Usuario? filter = null,
         Func<IQueryable<Usuario>,IIncludableQueryable<Usuario,object>>? include = null)
    {
        if (filter is null)
        {
            var ret = await GetList(include: source => source.Include(x => x.UsuarioTipoObj)!);
            return ret;
        }

        Expression<Func<Usuario, bool>> predicate = PredicateBuilder.New<Usuario>(true);

        if (filter.Id > 0)
            predicate = predicate.And(x => x.Id == filter.Id);
        
        if (!string.IsNullOrEmpty(filter.Nome))
            predicate = predicate.And(x => x.Nome == filter.Nome);
        
        if (!string.IsNullOrEmpty(filter.Email))
            predicate = predicate.And(x => x.Email == filter.Email);
        
        if (!string.IsNullOrEmpty(filter.Senha))
            predicate = predicate.And(x => x.Senha == filter.Senha);
        
        if (filter.UsuarioTipo > 0)
            predicate = predicate.And(x => x.UsuarioTipo == filter.UsuarioTipo);
        
        return await GetList(predicate, include: source => source.Include(x => x.UsuarioTipoObj)!);
    }

    public async Task<Usuario?> ObterPorId(long id,
        Func<IQueryable<Usuario>, IIncludableQueryable<Usuario, object>>? include = null)
    {
        return await GetSingle(x => x.Id == id, include: source => source.Include(x => x.UsuarioTipoObj)!);
    }
}