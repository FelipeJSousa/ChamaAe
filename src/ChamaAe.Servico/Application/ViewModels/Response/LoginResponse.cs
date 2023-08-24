using System.ComponentModel;
using System.Runtime.Serialization;

namespace ChamaAe.Servico.Application.ViewModels.Response;

[DisplayName("Login")]
[DataContract(Name = "LoginResponse", Namespace = "")]
public class LoginResponse : ViewModelBase
{
    [DataMember(Name = "sucesso")]
    public bool Sucesso { get; set; }
    
    [DataMember(Name = "Nome")]
    public UsuarioResponse? UsuarioObj { get; set; }
}