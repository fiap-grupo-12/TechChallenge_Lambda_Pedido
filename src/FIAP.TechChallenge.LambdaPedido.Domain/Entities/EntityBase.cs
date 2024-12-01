using Amazon.DynamoDBv2.DataModel;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaPedido.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public abstract class EntityBase
    {
        [DynamoDBProperty("id")]
        public Guid Id { get; set; }
    }
}
