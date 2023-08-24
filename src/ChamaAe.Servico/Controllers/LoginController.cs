using System;
using System.Threading.Tasks;
using ChamaAe.Servico.Application.ViewModels.Request;
using ChamaAe.Servico.Application.ViewModels.Response;
using ChamaAe.Servico.Domain.Entities;
using ChamaAe.Servico.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChamaAe.Servico.Controllers;

[ApiController]
[Route("/Login")]
public class LoginController : ApiBaseController
{
    private readonly ILoginService _loginService;
    
    public LoginController(IServiceProvider serviceProvider, ILoginService loginService) : base(serviceProvider)
    {
        _loginService = loginService;
    }

    /// <summary>
    /// Realizar login, autenticação do usuário administrador.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(LoginResponse))]
    [Consumes("application/json")]
    [HttpPost("Adm")]
    public async Task<IActionResult> LoginAdministrador([FromBody]LoginRequest request)
    {
        var ret = await _loginService.LoginAdm(Mapear<Usuario>(request));

        if (ret is null)
        {
            NewNotification("Email/Senha", "Credenciais são inválidas.");
            return Response(default);
        }
        
        return Response(Mapear<LoginResponse>(ret));
    }
    
    /// <summary>
    /// Realizar login, autenticação do usuário colaborador.
    /// </summary>
    /// <returns></returns>
    [Produces("application/json", Type = typeof(LoginResponse))]
    [Consumes("application/json")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {
        var ret = await _loginService.Login(Mapear<Usuario>(request));

        if (ret is null)
        {
            NewNotification("Email/Senha", "Credenciais são inválidas.");
            return Response(default);
        }
        
        return Response(Mapear<LoginResponse>(ret));
    }
}