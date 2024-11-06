namespace FIAP.TechChallenge.LambdaPedido.Application.Models.Response
{
    public class StatusPagamentoResponse
    {
        public int Id { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string StatusPagamento { get; set; }
    }
}
