using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamaAe.Servico.Application.ViewModels.Request;
using ChamaAe.Servico.Application.ViewModels.Response;
using ChamaAe.Servico.Domain.Entities;
using ChamaAe.Servico.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChamaAe.Servico.Controllers;

[ApiController]
[Route("/Categorias")]
public class CategoriasController : ApiBaseController
{

    private readonly ICategoriaService _categoriaService;
    
    
    public CategoriasController(IServiceProvider services, ICategoriaService categoriaService) : base(services)
    {
        _categoriaService = categoriaService;
    }
    
    /// <summary>
    /// Obter todos Categorias.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(IEnumerable<CategoriaResponse>))]
    [Consumes("application/json")]
    [HttpGet]
    public async Task<IActionResult> TodosCategorias([FromQuery] CategoriaRequest request)
    {
        var ret = await _categoriaService.ListarTodos(Mapear<Categoria>(request));        
        return ret.Any() ? Response(Mapear<IEnumerable<CategoriaResponse>>(ret)) : NoContent();
    }
    
    /// <summary>
    /// Salvar nova Categoria.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", "application/xml", Type = typeof(CategoriaResponse))]
    [Consumes("application/json", "application/xml")]
    [HttpPost("Salvar")]
    public async Task<IActionResult> SalvarCategoria(CategoriaCreateRequest request)
    {
        var ret = await _categoriaService.Salvar(Mapear<Categoria>(request));
        
        if(ret is not null)
            NewNotification("Categoria", "A categoria foi salva com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<CategoriaResponse>(ret));
    }
    
    /// <summary>
    /// Atualizar Categoria existente.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(CategoriaResponse))]
    [Consumes("application/json")]
    [HttpPut("Alterar")]
    public async Task<IActionResult> AtualizarCategoria(CategoriaAlterRequest request)
    {
        var ret = await _categoriaService.Alterar(Mapear<Categoria>(request));
        
        if(ret is not null)
            NewNotification("Categoria", "A categoria foi atualizada com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<CategoriaResponse>(ret));
    }
    
    /// <summary>
    /// Deletar Categoria existente.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(CategoriaResponse))]
    [Consumes("application/json")]
    [HttpDelete("Excluir/{Id:long}")]
    public async Task<IActionResult> DeletarCategoria([FromRoute] CategoriaDeleteRequest request)
    {
        var ret = await _categoriaService.Deletar(Mapear<Categoria>(request));

        if (ret is not null)
            NewNotification("Categoria", "A categoria foi excluída com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<CategoriaResponse>(ret));
    }
    
}