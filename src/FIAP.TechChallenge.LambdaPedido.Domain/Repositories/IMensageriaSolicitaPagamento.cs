namespace FIAP.TechChallenge.LambdaPedido.Domain.Repositories
{
    public interface IMensageriaSolicitaPagamento
    {
        Task SendMessage(string body);
    }
}
