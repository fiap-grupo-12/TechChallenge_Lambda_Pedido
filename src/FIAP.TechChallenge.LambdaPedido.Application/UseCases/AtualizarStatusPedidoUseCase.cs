using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities;
using FIAP.TechChallenge.LambdaPedido.Domain.Repositories;

namespace FIAP.TechChallenge.LambdaPedido.Application.UseCases
{
    public class AtualizarStatusPedidoUseCase : IAtualizarStatusPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        public AtualizarStatusPedidoUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        async Task<bool> IUseCaseAsync<AtualizarStatusPedidoRequest, bool>.Execute(AtualizarStatusPedidoRequest request)
        {
            Pedido pedido = await _pedidoRepository.GetById(request.Id);
            if (pedido != null)
            {
                pedido.StatusPedido = request.StatusPedido;
                await _pedidoRepository.Update(pedido, pedido.Id);
                return true;
            } else
            {
                return false;
            }
            
        }
    }
}
