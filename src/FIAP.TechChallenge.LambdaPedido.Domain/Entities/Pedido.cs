using Amazon.DynamoDBv2.DataModel;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;

namespace FIAP.TechChallenge.LambdaPedido.Domain.Entities
{
    [DynamoDBTable("pedidos")]
    public class Pedido
    {
        [DynamoDBHashKey("id")]
        public int Id { get; set; }

        [DynamoDBProperty("id_guid")]
        public Guid IdGuid { get; set; }

        [DynamoDBProperty("cliente")]
        public Cliente Cliente { get; set; }

        [DynamoDBProperty("forma_pagamento")]
        public FormaPagamento FormaPagamento { get; set; }

        [DynamoDBProperty("itens_pedido")]
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
