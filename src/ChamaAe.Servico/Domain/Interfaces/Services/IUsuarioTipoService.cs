using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamaAe.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace ChamaAe.Servico.Domain.Interfaces.Services;

public interface IUsuarioTipoService : IService
{
    Task<UsuarioTipo?> Deletar(UsuarioTipo obj);

    Task<UsuarioTipo?> Alterar(UsuarioTipo obj);

    Task<UsuarioTipo?> Salvar(UsuarioTipo obj);
    
    Task<IEnumerable<UsuarioTipo>> ListarTodos(UsuarioTipo? filter = null,
        Func<IQueryable<UsuarioTipo>, IIncludableQueryable<UsuarioTipo, object>>? include = null);

    Task<UsuarioTipo?> ObterPorId(long id,
        Func<IQueryable<UsuarioTipo>, IIncludableQueryable<UsuarioTipo, object>>? include = null);
}