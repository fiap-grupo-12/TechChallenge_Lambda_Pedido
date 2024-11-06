using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FIAP.TechChallenge.LambdaPedido.Application.Models.Request
{
    public class AtualizarStatusPedidoRequest
    {
        [Required(ErrorMessage = "É obrigatório informar o id.")]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "É obrigatório informar o status do pedido.")]
        [JsonPropertyName("statusPedido")]
        public StatusPedido StatusPedido { get; set; }
    }
}
