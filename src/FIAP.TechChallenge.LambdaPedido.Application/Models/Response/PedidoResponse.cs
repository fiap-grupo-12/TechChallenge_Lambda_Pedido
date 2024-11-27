using System.Text.Json.Serialization;

namespace FIAP.TechChallenge.LambdaPedido.Application.Models.Response
{
    public class PedidoResponse
    {
        //[JsonPropertyName("id")]
        //public int Id { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("dataCriacao")]
        public DateTime? DataCriacao { get; set; }

        [JsonPropertyName("statusPedido")]
        public string StatusPedido { get; set; }

        [JsonPropertyName("StatusPagamento")]
        public string StatusPagamento { get; set; }

        [JsonPropertyName("valorTotal")]
        public decimal ValorTotal { get; set; }

        [JsonPropertyName("cliente")]
        public ClienteResponse Cliente { get; set; }

        [JsonPropertyName("FormaPagamento")]
        public FormaPagamentoResponse FormaPagamento { get; set; }

        [JsonPropertyName("itensDoPedido")]
        public IList<ItensDePedidoResponse> ItensDePedido { get; set; }
    }

    public class ClienteResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("cpf")]
        public string CPF { get; set; }
    }

    public class FormaPagamentoResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }

    public class ItensDePedidoResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("quantidade")]
        public int Quantidade { get; set; }

        [JsonPropertyName("valorUnitario")]
        public decimal Valor { get; set; }
    }
}
