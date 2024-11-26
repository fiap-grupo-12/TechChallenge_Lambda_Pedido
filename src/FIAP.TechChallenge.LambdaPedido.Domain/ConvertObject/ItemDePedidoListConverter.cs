using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities;
using Newtonsoft.Json;

namespace FIAP.TechChallenge.LambdaPedido.Domain.ConvertObject
{
    public class ItemDePedidoListConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            var items = value as IList<ItemDePedido>;
            if (items == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            var entries = new DynamoDBList();
            foreach (var item in items)
            {
                entries.Add(Document.FromJson(JsonConvert.SerializeObject(item)));
            }
            return entries;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            var list = entry as DynamoDBList;
            if (list == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            var items = new List<ItemDePedido>();
            foreach (var item in list.Entries)
            {
                items.Add(JsonConvert.DeserializeObject<ItemDePedido>((item as Document).ToJson()));
            }
            return items;
        }

    }

}
