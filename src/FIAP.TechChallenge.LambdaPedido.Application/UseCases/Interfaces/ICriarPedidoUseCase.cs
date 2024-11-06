using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Response;

namespace FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces
{
    public interface ICriarPedidoUseCase : IUseCaseAsync<CriarPedidoRequest, PedidoResponse>
    {
    }
}
