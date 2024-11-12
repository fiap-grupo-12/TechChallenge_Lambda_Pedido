using AutoMapper;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Response;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaPedido.Domain.Repositories;

namespace FIAP.TechChallenge.LambdaPedido.Application.UseCases
{
    public class ObterPedidoPorIdUseCase : IObterPedidoPorIdUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public ObterPedidoPorIdUseCase(
            IPedidoRepository pedidoRepository,
            IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<PedidoResponse> Execute(int Id)
        {
            var result = await _pedidoRepository.GetById(Id);

            return _mapper.Map<PedidoResponse>(result);
        }
    }
}