using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamaAe.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace ChamaAe.Servico.Domain.Interfaces.Services;

public interface IUsuarioService : IService
{
    Task<Usuario?> Deletar(Usuario obj);

    Task<Usuario?> Alterar(Usuario obj);

    Task<Usuario?> Salvar(Usuario obj);
    
    Task<IEnumerable<Usuario>> ListarTodos(Usuario? filter = null,
        Func<IQueryable<Usuario>, IIncludableQueryable<Usuario, object>>? include = null);

    Task<Usuario?> ObterPorId(long id,
        Func<IQueryable<Usuario>, IIncludableQueryable<Usuario, object>>? include = null);
}