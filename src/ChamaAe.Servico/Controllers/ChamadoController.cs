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

[Route("/Chamado")]
public class ChamadoController : ApiBaseController
{
    private readonly IChamadoService _chamadoService;
    
    public ChamadoController(IServiceProvider services, IChamadoService chamadoService) : base(services)
    {
        _chamadoService = chamadoService;
    }
    
    /// <summary>
    /// Obter situação de Chamados.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(IEnumerable<ChamadoSituacaoResponse>))]
    [Consumes("application/json")]
    [HttpGet("Situacao")]
    public async Task<IActionResult> TodosChamadoSituacoes()
    {
        return Response(await Task.FromResult(Mapear<IEnumerable<ChamadoSituacaoResponse>>(Enum.GetValues(typeof(SituacaoChamado)).Cast<SituacaoChamado>())));
    }
    
    
    /// <summary>
    /// Obter todos Chamados.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(IEnumerable<ChamadoResponse>))]
    [Consumes("application/json")]
    [HttpGet]
    public async Task<IActionResult> TodosChamados([FromQuery] ChamadoRequest request)
    {
        var ret = await _chamadoService.ListarTodos(Mapear<Chamado>(request));        
        
        return ret.Any() ? Response(Mapear<IEnumerable<ChamadoResponse>>(ret)) : NoContent();
    }
    
    /// <summary>
    /// Salvar novo Chamado.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", "application/xml", Type = typeof(ChamadoResponse))]
    [Consumes("application/json", "application/xml")]
    [HttpPost("Salvar")]
    public async Task<IActionResult> SalvarChamado([FromBody] ChamadoCreateRequest request)
    {
        var ret = await _chamadoService.NovoChamado(Mapear<Chamado>(request));
        
        if(ret is not null)
            NewNotification("Chamado", "O Chamado foi salvo com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<ChamadoResponse>(ret));
    }
    
    /// <summary>
    /// Encerrar o Chamado.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(ChamadoResponse))]
    [Consumes("application/json")]
    [HttpPost("Encerrar")]
    public async Task<IActionResult> EncerrarChamado([FromBody] ChamadoEncerrarRequest request)
    {
        var ret = await _chamadoService.Encerrar(request.Id, request.Solucao);
        
        if(ret is not null)
            NewNotification("Chamado", "O Chamado foi Encerrado com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<ChamadoResponse>(ret));
    }
    
    /// <summary>
    /// Atribuir um usuário responsável para assumir o chamado.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(ChamadoResponse))]
    [Consumes("application/json")]
    [HttpPost("Assumir")]
    public async Task<IActionResult> AssumirChamado([FromBody] ChamadoAssumirRequest request)
    {
        var ret = await _chamadoService.Assumir(request.Id, request.UsuarioReponsavel);
        
        if(ret is not null)
            NewNotification("Chamado", "O Chamado foi assumido com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<ChamadoResponse>(ret));
    }
    
    /// <summary>
    /// Atualizar Chamado existente.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(ChamadoResponse))]
    [Consumes("application/json")]
    [HttpPut("Alterar")]
    public async Task<IActionResult> AtualizarChamado([FromBody] ChamadoAlterRequest request)
    {
        var ret = await _chamadoService.Alterar(Mapear<Chamado>(request));
        
        if(ret is not null)
            NewNotification("Chamado", "O Chamado foi atualizado com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<ChamadoResponse>(ret));
    }
    
    /// <summary>
    /// Deletar Chamado existente.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(ChamadoResponse))]
    [Consumes("application/json")]
    [HttpDelete("Excluir/{Id:long}")]
    public async Task<IActionResult> DeletarChamado([FromRoute] ChamadoDeleteRequest request)
    {
        var ret = await _chamadoService.Deletar(Mapear<Chamado>(request));

        if (ret is not null)
            NewNotification("Chamado", "O Chamado foi excluído com sucesso.", NotificationType.Information);
        
        return ret is null ? Response(default) : Response(Mapear<ChamadoResponse>(ret));
    }
    
}