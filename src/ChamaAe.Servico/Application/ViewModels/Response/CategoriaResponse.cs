using System.ComponentModel;
using System.Runtime.Serialization;

namespace ChamaAe.Servico.Application.ViewModels.Response;

[DisplayName("Categoria")]
[DataContract(Name = "CategoriaResponse", Namespace = "")]
public class CategoriaResponse : ViewModelBase
{
    [DataMember(Name = "Id")]
    public long Id { get; set; }
    
    [DataMember(Name = "Nome")]
    public string Nome { get; set; }
    
    [DataMember(Name = "Descricao")]
    public string Descricao { get; set; }
}