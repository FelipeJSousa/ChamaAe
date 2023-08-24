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

public class ChamadoService : ServiceBase<Chamado>, IChamadoService
{
    private readonly IUsuarioService _usuarioService;
    
    public ChamadoService(IServiceProvider serviceProvider, IRepository<Chamado> repoBase, IUsuarioService usuarioService) : base(serviceProvider, repoBase)
    {
        _usuarioService = usuarioService;
    }

    public async Task<Chamado?> Assumir(long idChamado, long idResponsavel)
    {
        var resp = await _usuarioService.ObterPorId(idResponsavel);

        if (resp.UsuarioTipo != 2)
        {
            NewNotification("UsuarioResponsavel", "O chamado não pode ser assumido por um usuário que não seja ADM.");
            return default;
        }
        
        var obj = await GetSingle(x => x.Id == idChamado);

        if (obj.Situacao != SituacaoChamado.Pendente)
        {
            NewNotification("Situacao", "Não é possível assumir um chamado que não esteja na situação Pendente.");
            return default;
        }

        obj.Situacao = SituacaoChamado.EmAtendimento;
        obj.UsuarioResponsavel = idResponsavel;

        await Save(obj);

        var ret = await ObterPorId(obj.Id);
        
        return ret;
    }

    public async Task<Chamado?> Deletar(Chamado obj)
    {
        var ret = await GetSingle(x => x.Id == obj.Id);

        if (ret is not null) return await Delete(ret, false);
        
        NewNotification("Chamado", "Não foi possível encontrar o Chamadocom o id " + obj.Id + ".");
        return null;
    }

    public async Task<Chamado?> Encerrar(long idChamado, string solucao)
    {
        var ret = await GetSingle(x => x.Id == idChamado);

        if (ret is not null)
        {
            ret.DataEncerramento = DateTime.Now;
            ret.Situacao = SituacaoChamado.Atendido;
            ret.Solucao = solucao;
            await Save(ret);
            ret = await ObterPorId(idChamado);
            return ret;
        }
        
        NewNotification("Chamado", "Não foi possível encontrar o Chamadocom o id " + idChamado + ".");
        return null;
    }

    public async Task<Chamado?> Alterar(Chamado obj)
    {
        var ret = await GetSingle(x => x.Id == obj.Id);

        if (ret is not null)
        {
            await Save(obj);
            ret = await ObterPorId(ret.Id);
            return ret;
        }
        
        NewNotification("Chamado", "Não foi possível encontrar o Chamadocom o id " + obj.Id + ".");
        return null;
    }

    public async Task<Chamado?> NovoChamado(Chamado obj)
    {
        if (obj is not null)
        {
            obj.Situacao = SituacaoChamado.Pendente; 
            obj.DataCriacao = DateTime.Now;
            
            await Save(obj);

            var ret = await ObterPorId(obj.Id);
            
            return ret;
        }
        
        NewNotification("Chamado", "Não é possivel salvar um objeto nulo.");
        return null;
    }

    public async Task<IEnumerable<Chamado>> ListarTodos(Chamado? filter = null,
         Func<IQueryable<Chamado>,IIncludableQueryable<Chamado,object>>? include = null)
    {
        if (filter is null)
            return await ListarDetalhado();

        return await ListarDetalhado(filter.getPredicate());
    }

    public async Task<IEnumerable<Chamado>> ListarDetalhado(Expression<Func<Chamado, bool>>? predicate = null)
    {
        predicate ??= PredicateBuilder.New<Chamado>(true);

        return await GetList(
            predicate,
            include: source => source
                .Include(x => x.UsuarioResponsavelObj)!
                .Include(x => x.UsuarioSolicitanteObj)!
                .Include(x => x.CategoriaObj)!
        );
    }


    public async Task<Chamado?> ObterPorId(long id,
        Func<IQueryable<Chamado>, IIncludableQueryable<Chamado, object>>? include = null)
    {
        return await GetSingle(x => x.Id == id,
            include: source => source
                .Include(x => x.UsuarioResponsavelObj).ThenInclude(x => x!.UsuarioTipoObj)
                .Include(x => x.UsuarioSolicitanteObj).ThenInclude(x => x!.UsuarioTipoObj)
                .Include(x => x.CategoriaObj)!);
    }
}