using AutoMapper;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Response;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;
using FIAP.TechChallenge.LambdaPedido.Domain.Repositories;

namespace FIAP.TechChallenge.LambdaPedido.Application.UseCases
{
    public class CriarPedidoUseCase(
        IPedidoRepository pedidoRepository,
        IMapper mapper) : ICriarPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<PedidoResponse> Execute(CriarPedidoRequest request)
        {
            var pedido = new Pedido()
            {
                Cliente = _mapper.Map<Cliente>(request.Cliente),
                FormaPagamento = _mapper.Map<FormaPagamento>(request.FormaPagamento),
                ItensDePedido = _mapper.Map<List<ItemDePedido>>(request.ItensDoPedido),
                DataCriacao = DateTime.Now,
                StatusPedido = StatusPedido.Recebido,
                StatusPagamento = StatusPagamento.Pendente
            };

            var result = await _pedidoRepository.Post(pedido);

            return _mapper.Map<PedidoResponse>(result);
        }

    }
}
