using Amazon.DynamoDBv2.DataModel;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;
using FIAP.TechChallenge.LambdaPedido.Domain.Repositories;

namespace FIAP.TechChallenge.LambdaPedido.Infra.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IDynamoDBContext _context;

        public PedidoRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<IList<Pedido>> GetAll()
        {
            try
            {
                return await _context.ScanAsync<Pedido>(default).GetRemainingAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedidos. {ex}");
            }
        }

        public async Task<Pedido> GetById(int Id)
        {
            try
            {
                return await _context.LoadAsync<Pedido>(Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedido {Id}. {ex}");
            }
        }

        public async Task<IList<Pedido>> GetByStatus(StatusPedido status)
        {
            try
            {
                var condition = new List<ScanCondition>()
                {
                    new ScanCondition("status",Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal,status)
                };

                return await _context.ScanAsync<Pedido>(condition).GetRemainingAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedidos. {ex}");
            }
        }

        public async Task<IList<Pedido>> GetFiltrados()
        {
            try
            {
                var condition = new List<ScanCondition>()
                {
                    new ScanCondition("status",Amazon.DynamoDBv2.DocumentModel.ScanOperator.NotEqual,StatusPedido.Finalizado)
                };

                return await _context.ScanAsync<Pedido>(condition).GetRemainingAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedidos filtrados. {ex}");
            }
        }

        public async Task<Pedido> Post(Pedido pedido)
        {
            try
            {
                pedido.IdGuid = Guid.NewGuid();

                await _context.SaveAsync(pedido);

                var condition = new List<ScanCondition>()
                {
                    new ScanCondition("id_guid", Amazon.DynamoDBv2.DocumentModel.ScanOperator.NotEqual,pedido.IdGuid)
                };

                var result = await _context.ScanAsync<Pedido>(condition).GetRemainingAsync();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar pedido. {ex.Message}", ex);
            }
        }

        public async Task Update(Pedido pedido, int Id)
        {
            try
            {
                await _context.SaveAsync(pedido);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar pedido. {ex}");
            }
        }
    }
}
