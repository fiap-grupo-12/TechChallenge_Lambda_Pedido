using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;

namespace FIAP.TechChallenge.LambdaPedido.Tests.Mock
{
    public static class AtualizarStatusPedidoRequestMock
    {
        public static AtualizarStatusPedidoRequest AtualizarStatusPedidoRequestFaker(StatusPedido status) =>
            new AtualizarStatusPedidoRequest
            {
                Id = new Random().Next(),
                StatusPedido = status
            };
    }
}
