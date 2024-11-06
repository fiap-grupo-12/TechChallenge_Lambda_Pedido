using FIAP.TechChallenge.LambdaPedido.Application.Models.Response;

namespace FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces
{
    public interface IObterPedidoPorIdUseCase : IUseCase<int, PedidoResponse>
    {
    }
}
