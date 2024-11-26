using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Core;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Response;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using System.Diagnostics.CodeAnalysis;
using FromBodyAttribute = Amazon.Lambda.Annotations.APIGateway.FromBodyAttribute;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FIAP.TechChallenge.LambdaPedido.API;

[ExcludeFromCodeCoverage]
public class Function
{
    private readonly IObterPedidosUseCase _obterPedidos;
    private readonly IObterPedidosFiltradosUseCase _obterPedidosFiltrados;
    private readonly IObterPedidoPorIdUseCase _obterPedidoPorId;
    private readonly IObterStatusPagamentoPorIdUseCase _obterStatusPagamentoPorId;
    private readonly ICriarPedidoUseCase _criarPedido;
    private readonly IAtualizarStatusPedidoUseCase _atualizarStatusPedido;
    private readonly IAtualizarStatusPagamentoUseCase _atualizarStatusPagamento;

    public Function(
            IObterPedidosUseCase obterPedidos,
            IObterPedidosFiltradosUseCase obterPedidosFiltrados,
            IObterPedidoPorIdUseCase obterPedidoPorId,
            IObterStatusPagamentoPorIdUseCase obterStatusPagamentoPorIdUseCase,
            ICriarPedidoUseCase criarPedido,
            IAtualizarStatusPedidoUseCase atualizarStatusPedido,
            IAtualizarStatusPagamentoUseCase atualizarStatusPagamento)
    {
        _obterPedidos = obterPedidos;
        _obterPedidosFiltrados = obterPedidosFiltrados;
        _obterPedidoPorId = obterPedidoPorId;
        _obterStatusPagamentoPorId = obterStatusPagamentoPorIdUseCase;
        _criarPedido = criarPedido;
        _atualizarStatusPedido = atualizarStatusPedido;
        _atualizarStatusPagamento = atualizarStatusPagamento;
    }

    [LambdaFunction(ResourceName = "CriarPedido")]
    [HttpApi(LambdaHttpMethod.Post, "/Pedido")]
    public async Task<PedidoResponse> CriarPedido([FromBody] CriarPedidoRequest request)
        => await _criarPedido.Execute(request);

    [LambdaFunction(ResourceName = "ObterPedidoPorId")]
    [HttpApi(LambdaHttpMethod.Get, "/Pedido/{id}")]
    public async Task<PedidoResponse> GetPedidoPorId(Guid id)
        => await _obterPedidoPorId.Execute(id);

    [LambdaFunction(ResourceName = "ListarPedidos")]
    [HttpApi(LambdaHttpMethod.Get, "/Pedido")]
    public async Task<IList<PedidoResponse>> GetPedidos(ILambdaContext context)
        => await _obterPedidos.Execute();

    [LambdaFunction(ResourceName = "ListarPedidosFiltrados")]
    [HttpApi(LambdaHttpMethod.Get, "/Pedido/Filtrados")]
    public async Task<IList<PedidoResponse>> GetFiltrados(ILambdaContext context)
        => await _obterPedidosFiltrados.Execute();

    [LambdaFunction(ResourceName = "StatusDoPagamentoPorId")]
    [HttpApi(LambdaHttpMethod.Get, "/Pedido/StatusPagamento/{id}")]
    public async Task<StatusPagamentoResponse> GetStatusPag(Guid id)
        => await _obterStatusPagamentoPorId.Execute(id);

    [LambdaFunction(ResourceName = "AtualizarStatusDoPedido")]
    [HttpApi(LambdaHttpMethod.Put, "/Pedido/StatusPedido")]
    public async Task<bool> PutStatusPedido([FromBody] AtualizarStatusPedidoRequest request)
        => await _atualizarStatusPedido.Execute(request);
}
