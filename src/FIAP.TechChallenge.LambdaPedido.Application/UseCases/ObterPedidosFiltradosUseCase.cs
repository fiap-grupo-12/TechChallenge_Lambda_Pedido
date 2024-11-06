using AutoMapper;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Response;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaPedido.Domain.Repositories;

namespace FIAP.TechChallenge.LambdaPedido.Application.UseCases
{
    public class ObterPedidosFiltradosUseCase : IObterPedidosFiltradosUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public ObterPedidosFiltradosUseCase(
            IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public IList<PedidoResponse> Execute()
        {
            var result = _pedidoRepository.GetFiltrados();

            return _mapper.Map<IList<PedidoResponse>>(result);
        }
    }
}