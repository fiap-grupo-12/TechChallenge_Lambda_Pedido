using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Response;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
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

    [LambdaFunction(ResourceName = "Handler")]
    public async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        bool methodOk = false;
        List<object> parameters = new List<object>();

        LambdaHttpMethod httpMethod = Enum.Parse<LambdaHttpMethod>(request.HttpMethod, true);
        try
        {
            foreach (var method in this.GetType().GetMethods().Where(x => x.Name != "Handler"))
            {
                foreach (var attributes in method.CustomAttributes.Where(x => x.ConstructorArguments.Count > 1))
                {
                    int methodType = (int)attributes.ConstructorArguments.FirstOrDefault(x => x.ArgumentType.Name == "LambdaHttpMethod").Value;
                    var pathType = attributes.ConstructorArguments.FirstOrDefault(x => x.ArgumentType.Name == "String").Value.ToString();

                    methodOk = httpMethod == (LambdaHttpMethod)methodType && string.Equals(pathType, request.Resource, StringComparison.CurrentCultureIgnoreCase);
                }
                if (methodOk)
                {
                    foreach (var parameter in method.GetParameters())
                        if (parameter.CustomAttributes.Count() > 0)
                            parameters.Add(Newtonsoft.Json.JsonConvert.DeserializeObject(request.Body, Type.GetType(parameter.ParameterType.AssemblyQualifiedName)));
                        else
                            foreach (var stringParameters in request.QueryStringParameters.Where(x => x.Key == parameter.Name))
                                parameters.Add(stringParameters.Value);

                    var resultAsync = method.Invoke(this, [.. parameters]);

                    if (resultAsync is Task task)
                    {
                        await task;
                        var resultProperty = task.GetType().GetProperty("Result");

                        return new APIGatewayProxyResponse
                        {
                            StatusCode = 200,
                            Body = Newtonsoft.Json.JsonConvert.SerializeObject(resultProperty?.GetValue(task)),
                            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                        };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 400,
                Body = ex.Message,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
        return null;
    }

    [HttpApi(LambdaHttpMethod.Post, "/Pedido")]
    public async Task<PedidoResponse> CriarPedido([FromBody] CriarPedidoRequest request)
        => await _criarPedido.Execute(request);

    [HttpApi(LambdaHttpMethod.Get, "/Pedido/{id}")]
    public async Task<PedidoResponse> GetPedidoPorId(string id)
        => await _obterPedidoPorId.Execute(Guid.Parse(id));

    [HttpApi(LambdaHttpMethod.Get, "/Pedido")]
    public async Task<IList<PedidoResponse>> GetPedidos()
        => await _obterPedidos.Execute();

    [HttpApi(LambdaHttpMethod.Get, "/Pedido/Filtrados")]
    public async Task<IList<PedidoResponse>> GetFiltrados()
        => await _obterPedidosFiltrados.Execute();

    [HttpApi(LambdaHttpMethod.Get, "/Pedido/StatusPagamento/{id}")]
    public async Task<StatusPagamentoResponse> GetStatusPag(string id)
        => await _obterStatusPagamentoPorId.Execute(Guid.Parse(id));

    [HttpApi(LambdaHttpMethod.Put, "/Pedido/StatusPedido")]
    public async Task<bool> PutStatusPedido([FromBody] AtualizarStatusPedidoRequest request)
        => await _atualizarStatusPedido.Execute(request);
}
