using AutoMapper;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Response;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaPedido.Domain.Repositories;

namespace FIAP.TechChallenge.LambdaPedido.Application.UseCases
{
    public class ObterPedidosUseCase : IObterPedidosUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public ObterPedidosUseCase(
            IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<IList<PedidoResponse>> Execute()
        {
            var result = await _pedidoRepository.GetAll();

            return _mapper.Map<IList<PedidoResponse>>(result);
        }
    }
}