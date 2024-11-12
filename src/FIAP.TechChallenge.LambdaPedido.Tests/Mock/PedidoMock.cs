using FIAP.TechChallenge.LambdaPedido.Domain.Entities;

namespace FIAP.TechChallenge.LambdaPedido.Tests.Mock
{
    public static class PedidoMock
    {
        public static Pedido PedidoFake() => new()
        {
            Id = new Random().Next(),
            IdGuid = new Guid(),
            DataCriacao = DateTime.Now,
            StatusPagamento = Domain.Entities.Enum.StatusPagamento.Pendente,
            Cliente = ClienteMock.ClienteFake(),
            FormaPagamento = FormaPagamentoMock.FormaPagamentoFake(),
            StatusPedido = Domain.Entities.Enum.StatusPedido.Recebido,
            ItensDePedido = ItemPedidoMock.ItensPedidoFake()
        };
    }
}
