using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamaAe.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace ChamaAe.Servico.Domain.Interfaces.Services;

public interface ICategoriaService : IService
{
    public Task<Categoria?> Alterar(Categoria obj);
    
    public Task<Categoria?> Deletar(Categoria obj);
    
    public Task<Categoria?> Salvar(Categoria obj);
    
    public Task<IEnumerable<Categoria>> ListarTodos(Categoria? filter = null, Func<IQueryable<Categoria>, IIncludableQueryable<Categoria, object>>? include = null);
    
    public Task<Categoria?> ObterPorId(long id, Func<IQueryable<Categoria>, IIncludableQueryable<Categoria, object>>? include = null);
}