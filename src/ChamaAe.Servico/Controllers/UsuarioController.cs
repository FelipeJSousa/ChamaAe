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

[Route("/Usuario")]
public class UsuarioController : ApiBaseController
{
private readonly IUsuarioService _usuarioService;
    
    
    public UsuarioController(IServiceProvider services, IUsuarioService usuarioService) : base(services)
    {
        _usuarioService = usuarioService;
    }
    
    /// <summary>
    /// Obter todos Usuarios.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(IEnumerable<UsuarioResponse>))]
    [Consumes("application/json")]
    [HttpGet]
    public async Task<IActionResult> TodosUsuarios([FromQuery] UsuarioRequest request)
    {
        var ret = await _usuarioService.ListarTodos(Mapear<Usuario>(request));        
        
        return ret.Any() ? Response(Mapear<IEnumerable<UsuarioResponse>>(ret)) : NoContent();
    }
    
    /// <summary>
    /// Salvar novo Usuario.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", "application/xml", Type = typeof(UsuarioResponse))]
    [Consumes("application/json", "application/xml")]
    [HttpPost("Salvar")]
    public async Task<IActionResult> SalvarUsuario([FromBody] UsuarioCreateRequest request)
    {
        var ret = await _usuarioService.Salvar(Mapear<Usuario>(request));

        if (Notifications.HasNotificationsErrors())
            return Response(default);
        
        NewNotification("Usuario", "O Usuario foi salvo com sucesso.", NotificationType.Information);
        
        return Response(Mapear<UsuarioResponse>(ret));
    }
    
    /// <summary>
    /// Atualizar Usuario existente.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(UsuarioResponse))]
    [Consumes("application/json")]
    [HttpPut("Alterar")]
    public async Task<IActionResult> AtualizarUsuario([FromBody] UsuarioAlterRequest request)
    {
        var ret = await _usuarioService.Alterar(Mapear<Usuario>(request));
        
        if(ret is not null && !Notifications.HasNotificationsErrors())
            NewNotification("Usuario", "O Usuario foi atualizada com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<UsuarioResponse>(ret));
    }
    
    /// <summary>
    /// Deletar Usuario existente.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(UsuarioResponse))]
    [Consumes("application/json")]
    [HttpDelete("Excluir/{Id:long}")]
    public async Task<IActionResult> DeletarUsuario([FromRoute] UsuarioDeleteRequest request)
    {
        var ret = await _usuarioService.Deletar(Mapear<Usuario>(request));

        if (ret is not null && !Notifications.HasNotificationsErrors())
            NewNotification("Usuario", "O Usuario foi excluída com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<UsuarioResponse>(ret));
    }
    
}