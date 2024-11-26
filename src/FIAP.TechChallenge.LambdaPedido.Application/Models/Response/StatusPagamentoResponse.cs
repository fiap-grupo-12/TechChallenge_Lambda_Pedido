namespace FIAP.TechChallenge.LambdaPedido.Application.Models.Response
{
    public class StatusPagamentoResponse
    {
        public Guid Id { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string StatusPagamento { get; set; }
    }
}
