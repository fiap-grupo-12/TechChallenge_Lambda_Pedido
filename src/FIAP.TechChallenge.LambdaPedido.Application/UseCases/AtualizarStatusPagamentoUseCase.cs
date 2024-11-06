using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities;
using FIAP.TechChallenge.LambdaPedido.Domain.Repositories;

namespace FIAP.TechChallenge.LambdaPedido.Application.UseCases
{
    public class AtualizarStatusPagamentoUseCase(IPedidoRepository pedidoRepository) : IAtualizarStatusPagamentoUseCase
    {
        async Task<bool> IUseCaseAsync<AtualizarStatusPagamentoRequest, bool>.Execute(AtualizarStatusPagamentoRequest request)
        {
            Pedido pedido = await pedidoRepository.GetById(request.PedidoId);

            if (pedido != null)
            {
                if (pedido.StatusPagamento == request.StatusPagamento) 
                    return true;

                pedido.StatusPagamento = request.StatusPagamento;
                await pedidoRepository.Update(pedido, pedido.Id);

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
