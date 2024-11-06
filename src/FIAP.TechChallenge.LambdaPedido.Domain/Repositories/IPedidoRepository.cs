using FIAP.TechChallenge.LambdaPedido.Domain.Entities;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;

namespace FIAP.TechChallenge.LambdaPedido.Domain.Repositories
{
    public interface IPedidoRepository
    {
        Task<Pedido> Post(Pedido pedido);
        Task Update(Pedido pedido, int Id);
        Task<Pedido> GetById(int Id);
        Task<IList<Pedido>> GetAll();
        Task<IList<Pedido>> GetFiltrados();
        Task<IList<Pedido>> GetByStatus(StatusPedido status);
    }
}
