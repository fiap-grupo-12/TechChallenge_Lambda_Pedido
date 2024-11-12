using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FIAP.TechChallenge.LambdaPedido.Application.Models.Request
{
    public class CriarPedidoRequest
    {
        [JsonPropertyName("valorTotal")]
        public double? ValorTotal { get; set; }

        [JsonPropertyName("cpf")]
        public ClienteRequest? Cliente { get; set; }

        [Required(ErrorMessage = "É obrigatório informar ao menos 1 PRODUTO para finalizar o pedido.")]
        [JsonPropertyName("produtoQuantidades")]
        public List<ItemDePedidoRequest> ItensDoPedido { get; set; }

        [Required(ErrorMessage = "É obrigatório informar uma FORMA DE PAGAMENTO para finalizar o pedido.")]
        [JsonPropertyName("idFormaPagamento")]
        public FormaPagamentoRequest FormaPagamento { get; set; }
    }

    public class FormaPagamentoRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }

    public class ClienteRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("cpf")]
        public string CPF { get; set; }
    }

    public class ItemDePedidoRequest
    {
        [Required(ErrorMessage = "É obrigatório informar ao menos 1 PRODUTO para finalizar o pedido.")]
        [JsonPropertyName("idProduto")]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "É obrigatório informar ao menos 1 PRODUTO para finalizar o pedido.")]
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório informar ao menos 1 PRODUTO para finalizar o pedido.")]
        [JsonPropertyName("quantidade")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "É obrigatório informar ao menos 1 PRODUTO para finalizar o pedido.")]
        [JsonPropertyName("valorUnitario")]
        public decimal ValorUnitario { get; set; }
    }

}
