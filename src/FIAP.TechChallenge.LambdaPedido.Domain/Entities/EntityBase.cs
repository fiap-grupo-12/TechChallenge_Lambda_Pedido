using Amazon.DynamoDBv2.DataModel;

namespace FIAP.TechChallenge.LambdaPedido.Domain.Entities
{
    public abstract class EntityBase
    {
        [DynamoDBProperty("id")]
        public int Id { get; set; }
    }
}
