using AutoMapper;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Response;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;
using FIAP.TechChallenge.LambdaPedido.Domain.Repositories;
using Newtonsoft.Json;

namespace FIAP.TechChallenge.LambdaPedido.Application.UseCases
{
    public class CriarPedidoUseCase(
        IPedidoRepository pedidoRepository,
        IMensageriaSolicitaPagamento mensageria,
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

            await mensageria.SendMessage(JsonConvert.SerializeObject(new
            {
                idPedido = pedido.Id,
                valor = pedido.ValorTotal
            }));

            return _mapper.Map<PedidoResponse>(result);
        }

    }
}
