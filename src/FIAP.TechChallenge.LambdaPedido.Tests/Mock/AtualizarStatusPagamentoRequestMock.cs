using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;

namespace FIAP.TechChallenge.LambdaPedido.Tests.Mock
{
    public static class AtualizarStatusPagamentoRequestMock
    {
        public static AtualizarStatusPagamentoRequest AtualizarStatusPagamentoRequestFaker(StatusPagamento status) =>
            new AtualizarStatusPagamentoRequest
            {
                PedidoId = new Guid(),
                StatusPagamento = status
            };
    }
}
