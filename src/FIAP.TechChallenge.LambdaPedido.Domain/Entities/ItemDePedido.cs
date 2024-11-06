using Amazon.DynamoDBv2.DataModel;

namespace FIAP.TechChallenge.LambdaPedido.Domain.Entities
{
    public class ItemDePedido : EntityBase
    {
        [DynamoDBProperty("nome")]
        public string Nome { get; set; }

        [DynamoDBProperty("valor")]
        public double Valor { get; set; }

        [DynamoDBProperty("quantidade")]
        public int Quantidade { get; set; }
    }

}
