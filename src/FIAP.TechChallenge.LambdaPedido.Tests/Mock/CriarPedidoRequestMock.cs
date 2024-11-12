using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;

namespace FIAP.TechChallenge.LambdaPedido.Tests.Mock
{
    public static class CriarPedidoRequestMock
    {
        public static CriarPedidoRequest CriarPedidoRequestFake() => new CriarPedidoRequest()
        {
            Cliente = ClienteMock.ClienteRequestFake(),
            FormaPagamento = FormaPagamentoMock.FormaPagamentoRequestFake(),
            ItensDoPedido = ItemPedidoMock.ItensDePedidoRequestFake(),
            ValorTotal = 44.16
        };
    }
}
