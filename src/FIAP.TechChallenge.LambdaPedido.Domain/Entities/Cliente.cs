using Amazon.DynamoDBv2.DataModel;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaPedido.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Cliente : EntityBase
    {
        [DynamoDBProperty("Nome")]
        public string Nome { get; set; }

        [DynamoDBProperty("CPF")]
        public string CPF { get; set; }
    }
}
