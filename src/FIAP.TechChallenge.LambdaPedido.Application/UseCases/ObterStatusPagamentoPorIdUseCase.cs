using AutoMapper;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Response;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaPedido.Domain.Repositories;

namespace FIAP.TechChallenge.LambdaPedido.Application.UseCases
{
    public class ObterStatusPagamentoPorIdUseCase : IObterStatusPagamentoPorIdUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public ObterStatusPagamentoPorIdUseCase(
            IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<StatusPagamentoResponse> Execute(int id)
        {
            var result = await _pedidoRepository.GetById(id);

            return _mapper.Map<StatusPagamentoResponse>(result);
        }
    }
}
