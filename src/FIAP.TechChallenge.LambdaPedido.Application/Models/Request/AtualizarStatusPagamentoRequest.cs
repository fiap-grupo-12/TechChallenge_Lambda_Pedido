using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FIAP.TechChallenge.LambdaPedido.Application.Models.Request
{
    public class AtualizarStatusPagamentoRequest
    {
        [Required(ErrorMessage = "É obrigatório informar o id do pedido.")]
        [JsonPropertyName("PedidoId")]
        public Guid PedidoId { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o status do pagamento.")]
        public StatusPagamento StatusPagamento { get; set; }
    }
}
