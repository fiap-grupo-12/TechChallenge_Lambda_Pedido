using System.ComponentModel;

namespace FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum
{
    public enum StatusPagamento
    {
        [Description("Pendente")]
        Pendente = 0,

        [Description("Aprovado")]
        Aprovado = 1,

        [Description("Recusado")]
        Recusado = 2
    }
}
