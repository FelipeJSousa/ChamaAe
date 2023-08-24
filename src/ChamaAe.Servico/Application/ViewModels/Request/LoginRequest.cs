using System.ComponentModel;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ChamaAe.Servico.Application.ViewModels.Request;

[DisplayName("Login")]
[DataContract(Name = "LoginRequest", Namespace = "")]
public class LoginRequest : ViewModelBase
{
    [FromBody]
    [DataMember(Name = "Email")]
    public string Email { get; set; }
    
    [FromBody]
    [DataMember(Name = "Senha")]
    public string Senha { get; set; }
}
