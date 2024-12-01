using Bogus;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities;

namespace FIAP.TechChallenge.LambdaPedido.Tests.Mock
{
    public static class ItemPedidoMock
    {
        public static List<ItemDePedido> ItensPedidoFake() => new Faker<ItemDePedido>()
            .CustomInstantiator(f =>
                new ItemDePedido()
                {
                    Id = new Random().Next(),
                    Nome = f.Commerce.ProductName(),
                    Quantidade = new Random().Next(10),
                    Valor = new Random().Next(5, 20)
                }
            ).Generate(5);

        public static List<ItemDePedidoRequest> ItensDePedidoRequestFake() => new Faker<ItemDePedidoRequest>()
            .CustomInstantiator(f =>
                new ItemDePedidoRequest()
                {
                    Id = new Random().Next(),
                    Nome = f.Commerce.ProductName(),
                    Quantidade = new Random().Next(10),
                    Valor = new Random().Next(5, 20)
                }
            ).Generate(5);
    }
}
