using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities;

namespace FIAP.TechChallenge.LambdaPedido.Tests.Mock
{
    public static class ClienteMock
    {
        public static Cliente ClienteFake() => new Cliente()
        {
            CPF = "00000000000",
            Id = new Guid(),
            Nome = "João da Silva"
        };

        public static ClienteRequest ClienteRequestFake() => new ClienteRequest()
        {
            CPF = "00000000000",
            Id = new Guid(),
            Nome = "João da Silva"
        };
    }
}
