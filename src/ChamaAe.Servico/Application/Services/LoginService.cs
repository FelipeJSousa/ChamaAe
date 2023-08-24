using System;
using System.Linq;
using System.Threading.Tasks;
using ChamaAe.Servico.Domain.Entities;
using ChamaAe.Servico.Domain.Interfaces.Services;

namespace ChamaAe.Servico.Application.Services;

public class LoginService : ServiceBase, ILoginService
{
    private readonly IUsuarioService _usuarioService;
    private readonly IUsuarioTipoService _usuarioTipoService;
    
    public LoginService(IServiceProvider serviceProvider, IUsuarioService usuarioService, IUsuarioTipoService usuarioTipoService) : base(serviceProvider)
    {
        _usuarioService = usuarioService;
        _usuarioTipoService = usuarioTipoService;
    }

    public async Task<Usuario?> LoginAdm(Usuario obj)
    {
        obj.UsuarioTipo = (await _usuarioTipoService.ListarTodos(new UsuarioTipo() {Nome = "ADM"})).FirstOrDefault()?.Id;
        if (obj.UsuarioTipo is null)
        {
            NewNotification("UsuarioTipo", "Não foi possível obter o tipo usuário ADM.");
            return default;
        }
        return (await _usuarioService.ListarTodos(obj)).FirstOrDefault();
    }
    
    public async Task<Usuario?> Login(Usuario obj)
    {
        return (await _usuarioService.ListarTodos(obj)).FirstOrDefault();
    }
    
}