using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;

namespace FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces
{
    public interface IAtualizarStatusPedidoUseCase : IUseCaseAsync<AtualizarStatusPedidoRequest, bool>
    {
    }
}