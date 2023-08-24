using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ChamaAe.Servico.Domain.Extensions;

namespace ChamaAe.Servico.Domain.Entities;

public enum SituacaoChamado
{
    [DataMember(Name = "Pendente")]
    [Description("Pendente")]
    Pendente = 1, 
    
    [DataMember(Name = "Em Atendimento")]
    [Description("Em Atendimento")]
    EmAtendimento = 2,
    
    [DataMember(Name = "Atendido")]
    [Description("Atendido")]
    Atendido = 3
}

public static class SituacaoChamadoExtensions{

    public static string ToString(this SituacaoChamado obj)
    {
        return obj.GetDescription();
    }
}