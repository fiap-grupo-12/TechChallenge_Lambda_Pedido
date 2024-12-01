using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FIAP.TechChallenge.LambdaPedido.Application.Models.Request
{
    public class CriarPedidoRequest
    {
        [JsonProperty("valorTotal")]
        public double? ValorTotal { get; set; }

        [JsonProperty("cliente")]
        public ClienteRequest? Cliente { get; set; }

        [Required(ErrorMessage = "É obrigatório informar ao menos 1 PRODUTO para finalizar o pedido.")]
        [JsonProperty("itensDoPedido")]
        public List<ItemDePedidoRequest> ItensDoPedido { get; set; }

        [Required(ErrorMessage = "É obrigatório informar uma FORMA DE PAGAMENTO para finalizar o pedido.")]
        [JsonProperty("formaPagamento")]
        public FormaPagamentoRequest FormaPagamento { get; set; }
    }

    public class FormaPagamentoRequest
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }

    public class ClienteRequest
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("cpf")]
        public string CPF { get; set; }
    }

    public class ItemDePedidoRequest
    {
        [Required(ErrorMessage = "É obrigatório informar ao menos 1 PRODUTO para finalizar o pedido.")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório informar ao menos 1 PRODUTO para finalizar o pedido.")]
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório informar ao menos 1 PRODUTO para finalizar o pedido.")]
        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "É obrigatório informar ao menos 1 PRODUTO para finalizar o pedido.")]
        [JsonProperty("valor")]
        public decimal Valor { get; set; }
    }

}
