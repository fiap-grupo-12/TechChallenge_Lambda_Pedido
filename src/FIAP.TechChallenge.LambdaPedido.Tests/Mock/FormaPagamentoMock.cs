using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities;

namespace FIAP.TechChallenge.LambdaPedido.Tests.Mock
{
    public static class FormaPagamentoMock
    {
        public static FormaPagamento FormaPagamentoFake() => new FormaPagamento()
        {
            Id = new Random().Next(),
            Nome = "PIX"
        };

        public static FormaPagamentoRequest FormaPagamentoRequestFake() => new FormaPagamentoRequest()
        {
            Id = new Random().Next(),
            Nome = "PIX"
        };
    }
}
