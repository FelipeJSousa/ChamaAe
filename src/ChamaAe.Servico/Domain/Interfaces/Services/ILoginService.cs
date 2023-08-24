using System.Threading.Tasks;
using ChamaAe.Servico.Domain.Entities;

namespace ChamaAe.Servico.Domain.Interfaces.Services;

public interface ILoginService : IService
{
    Task<Usuario?> LoginAdm(Usuario obj);
    
    Task<Usuario?> Login(Usuario obj);
}