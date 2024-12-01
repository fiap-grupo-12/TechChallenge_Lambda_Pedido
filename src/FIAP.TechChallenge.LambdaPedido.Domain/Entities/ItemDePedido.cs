using Amazon.DynamoDBv2.DataModel;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaPedido.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class ItemDePedido
    {
        [DynamoDBProperty("id")]
        public int Id { get; set; }

        [DynamoDBProperty("Nome")]
        public string Nome { get; set; }

        [DynamoDBProperty("Valor")]
        public double Valor { get; set; }

        [DynamoDBProperty("Quantidade")]
        public int Quantidade { get; set; }
    }

}
