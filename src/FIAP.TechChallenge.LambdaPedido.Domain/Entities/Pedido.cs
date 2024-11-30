using Amazon.DynamoDBv2.DataModel;
using FIAP.TechChallenge.LambdaPedido.Domain.ConvertObject;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaPedido.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    [DynamoDBTable("PedidoTable")]
    public class Pedido
    {
        //[DynamoDBHashKey("id")]
        //public int Id { get; set; }

        [DynamoDBHashKey("id")]
        public Guid Id { get; set; }

        [DynamoDBProperty("valor_total")]
        public double? ValorTotal { get; set; }

        [DynamoDBProperty("cliente")]
        public Cliente Cliente { get; set; }

        [DynamoDBProperty("forma_pagamento")]
        public FormaPagamento FormaPagamento { get; set; }

        [DynamoDBProperty(Converter = typeof(ItemDePedidoListConverter))]
        public IList<ItemDePedido> ItensDePedido { get; set; }

        [DynamoDBProperty("data_criacao")]
        public DateTime? DataCriacao { get; set; }

        [DynamoDBProperty("data_preparacao")]
        public DateTime? DataPreparacao { get; set; }

        [DynamoDBProperty("data_pronto")]
        public DateTime? DataPronto { get; set; }

        [DynamoDBProperty("data_encerrado")]
        public DateTime? DataEncerrado { get; set; }

        [DynamoDBProperty("status_pedido")]
        public StatusPedido StatusPedido { get; set; }

        [DynamoDBProperty("status_pagamento")]
        public StatusPagamento StatusPagamento { get; set; }
    }
}
