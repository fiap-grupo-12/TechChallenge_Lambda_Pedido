using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
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

        public async Task<Pedido> GetById(Guid Id)
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
                    new ScanCondition("StatusPedido",ScanOperator.Equal,status)
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
                    new ScanCondition("StatusPedido",ScanOperator.NotEqual,StatusPedido.Finalizado)
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
                pedido.Id = Guid.NewGuid();

                await _context.SaveAsync(pedido);

                var condition = new List<ScanCondition>()
                {
                    new ScanCondition("IdGuid", ScanOperator.Equal,pedido.Id)
                };

                var result = await _context.ScanAsync<Pedido>(condition).GetRemainingAsync();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar pedido. {ex.Message}", ex);
            }
        }

        public async Task Update(Pedido pedido, Guid Id)
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
