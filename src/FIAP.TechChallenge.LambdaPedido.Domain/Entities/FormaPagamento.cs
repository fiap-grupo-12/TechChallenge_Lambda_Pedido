using Amazon.DynamoDBv2.DataModel;

namespace FIAP.TechChallenge.LambdaPedido.Domain.Entities
{
    public class FormaPagamento : EntityBase
    {
        [DynamoDBProperty("nome")]
        public string Nome { get; set; }
    }
}
