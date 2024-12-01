using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaPedido.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class ItemDePedido
    {
        [DynamoDBProperty("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [DynamoDBProperty("nome")]
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [DynamoDBProperty("valor")]
        [JsonProperty("valor")]
        public double Valor { get; set; }

        [DynamoDBProperty("quantidade")]
        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }
    }

}
