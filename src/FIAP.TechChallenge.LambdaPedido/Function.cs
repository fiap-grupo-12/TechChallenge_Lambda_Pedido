using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FromBodyAttribute = Amazon.Lambda.Annotations.APIGateway.FromBodyAttribute;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FIAP.TechChallenge.LambdaPedido.API;

[ApiController]
[Route("api/Pedido")]
public class Function : Controller
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

    [HttpPost]
    [LambdaFunction(ResourceName = "CriarPedido")]
    [HttpApi(LambdaHttpMethod.Post, "/Pedido")]
    public async Task<IActionResult> CriarPedidoAsync([FromBody] CriarPedidoRequest request)
    {
        try
        {
            var retorno = await _criarPedido.Execute(request);
            return StatusCode(201, retorno);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    [LambdaFunction(ResourceName = "ObterPedidoPorId")]
    [HttpApi(LambdaHttpMethod.Get, "/Pedido/{id}")]
    public IActionResult GetPedidoPorId(int id)
    {
        try
        {
            var result = _obterPedidoPorId.Execute(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    [LambdaFunction(ResourceName = "ListarPedidos")]
    [HttpApi(LambdaHttpMethod.Get, "/Pedido")]
    public IActionResult GetPedidos(ILambdaContext context)
    {
        try
        {
            var result = _obterPedidos.Execute();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    [LambdaFunction(ResourceName = "ListarPedidosFiltrados")]
    [HttpApi(LambdaHttpMethod.Get, "/Pedido/Filtrados")]
    public IActionResult GetFiltrados(ILambdaContext context)
    {
        try
        {
            var result = _obterPedidosFiltrados.Execute();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    [LambdaFunction(ResourceName = "StatusDoPagamentoPorId")]
    [HttpApi(LambdaHttpMethod.Get, "/Pedido/StatusPagamento/{id}")]
    public IActionResult GetStatusPag(int id)
    {
        try
        {
            var result = _obterStatusPagamentoPorId.Execute(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPut]
    [LambdaFunction(ResourceName = "AtualizarStatusDoPedido")]
    [HttpApi(LambdaHttpMethod.Put, "/Pedido/StatusPedido")]
    public IActionResult PutStatusPedido([FromBody] AtualizarStatusPedidoRequest request)
    {
        try
        {
            _atualizarStatusPedido.Execute(request);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [LambdaFunction]
    public async Task PutStatusPag(SQSEvent sqsEvent)
    {
        foreach (var record in sqsEvent.Records)
        {
            Console.WriteLine($"[{record.EventSource}] Body = {record.Body}");
        }
    }
}
