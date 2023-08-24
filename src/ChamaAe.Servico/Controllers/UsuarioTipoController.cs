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
[Route("/UsuarioTipo")]
public class UsuarioTipoController : ApiBaseController
{
private readonly IUsuarioTipoService _usuarioTipoService;
    
    
    public UsuarioTipoController(IServiceProvider services, IUsuarioTipoService usuarioTipoService) : base(services)
    {
        _usuarioTipoService = usuarioTipoService;
    }
    
    /// <summary>
    /// Obter todos UsuarioTipos.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(IEnumerable<UsuarioTipoResponse>))]
    [Consumes("application/json")]
    [HttpGet]
    public async Task<IActionResult> TodosUsuarioTipos([FromQuery] UsuarioTipoRequest request)
    {
        var ret = await _usuarioTipoService.ListarTodos(Mapear<UsuarioTipo>(request));        
        return ret.Any() ? Response(Mapear<IEnumerable<UsuarioTipoResponse>>(ret)) : NoContent();
    }
    
    /// <summary>
    /// Salvar novo UsuarioTipo.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", "application/xml", Type = typeof(UsuarioTipoResponse))]
    [Consumes("application/json", "application/xml")]
    [HttpPost("Salvar")]
    public async Task<IActionResult> SalvarUsuarioTipo(UsuarioTipoCreateRequest request)
    {
        var ret = await _usuarioTipoService.Salvar(Mapear<UsuarioTipo>(request));
        
        if(ret is not null)
            NewNotification("UsuarioTipo", "O UsuarioTipo foi salva com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<UsuarioTipoResponse>(ret));
    }
    
    /// <summary>
    /// Atualizar UsuarioTipo existente.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(UsuarioTipoResponse))]
    [Consumes("application/json")]
    [HttpPut("Alterar")]
    public async Task<IActionResult> AtualizarUsuarioTipo(UsuarioTipoAlterRequest request)
    {
        var ret = await _usuarioTipoService.Alterar(Mapear<UsuarioTipo>(request));
        
        if(ret is not null)
            NewNotification("UsuarioTipo", "O UsuarioTipo foi atualizada com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<UsuarioTipoResponse>(ret));
    }
    
    /// <summary>
    /// Deletar UsuarioTipo existente.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(UsuarioTipoResponse))]
    [Consumes("application/json")]
    [HttpDelete("Excluir/{Id:long}")]
    public async Task<IActionResult> DeletarUsuarioTipo([FromRoute] UsuarioTipoDeleteRequest request)
    {
        var ret = await _usuarioTipoService.Deletar(Mapear<UsuarioTipo>(request));

        if (ret is not null)
            NewNotification("UsuarioTipo", "O UsuarioTipo foi excluída com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<UsuarioTipoResponse>(ret));
    }
    
}