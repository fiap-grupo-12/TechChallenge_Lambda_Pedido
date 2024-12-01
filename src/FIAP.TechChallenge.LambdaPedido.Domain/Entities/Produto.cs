using Amazon.DynamoDBv2.DataModel;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaPedido.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Produto
    {
        [DynamoDBProperty("id")]
        public int Id { get; set; }

        [DynamoDBProperty("nome")]
        public string Nome { get; set; }

        [DynamoDBProperty("valor")]
        public double Valor { get; set; }//{ get => String.Format("{0:0.00}", _valor); set => _valor = Convert.ToDouble(value); }

        //[DynamoDBProperty("categoria")]
        //public Categoria CategoriaProduto { get; set; }
    }
}
