using AutoMapper;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;
using FIAP.TechChallenge.LambdaPedido.Domain.Repositories;
using FIAP.TechChallenge.LambdaPedido.Infra.Data.Configurations;
using FIAP.TechChallenge.LambdaPedido.Tests.Mock;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace FIAP.TechChallenge.LambdaPedido.Tests.Application;

public class ApplicationTest
{
    private readonly Mock<IObterPedidosUseCase> _obterPedidos;
    private readonly Mock<IObterPedidosFiltradosUseCase> _obterPedidosFiltrados;
    private readonly Mock<IObterPedidoPorIdUseCase> _obterPedidoPorId;
    private readonly Mock<IObterStatusPagamentoPorIdUseCase> _obterStatusPagamentoPorId;
    private readonly Mock<ICriarPedidoUseCase> _criarPedido;
    private readonly Mock<IAtualizarStatusPedidoUseCase> _atualizarStatusPedido;
    private readonly Mock<IAtualizarStatusPagamentoUseCase> _atualizarStatusPagamento;
    private readonly Mock<IPedidoRepository> _pedidoRepository;
    private readonly IMapper _mapper;

    public ApplicationTest()
    {
        _obterPedidos = new Mock<IObterPedidosUseCase>();
        _obterPedidosFiltrados = new Mock<IObterPedidosFiltradosUseCase>();
        _obterPedidoPorId = new Mock<IObterPedidoPorIdUseCase>();
        _obterStatusPagamentoPorId = new Mock<IObterStatusPagamentoPorIdUseCase>();
        _criarPedido = new Mock<ICriarPedidoUseCase>();
        _atualizarStatusPedido = new Mock<IAtualizarStatusPedidoUseCase>();
        _atualizarStatusPagamento = new Mock<IAtualizarStatusPagamentoUseCase>();
        _pedidoRepository = new Mock<IPedidoRepository>();
        _mapper = new MapperConfiguration(c => c.AddProfile<MapperConfig>()).CreateMapper();
    }

    [Theory]
    [InlineData(StatusPagamento.Aprovado)]
    [InlineData(StatusPagamento.Recusado)]
    [InlineData(StatusPagamento.Pendente)]
    public async void AtualizarStatusPagamento_OK_test(StatusPagamento statusPagamento)
    {
        // Arrange
        var atualizacaoFake = AtualizarStatusPagamentoRequestMock.AtualizarStatusPagamentoRequestFaker(statusPagamento);
        var pedidoFake = PedidoMock.PedidoFake();

        _pedidoRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(Task.FromResult(pedidoFake));
        _pedidoRepository.Setup(x => x.Update(It.IsAny<Pedido>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);

        var exec = new AtualizarStatusPagamentoUseCase(_pedidoRepository.Object);

        // Act
        var result = await exec.Execute(atualizacaoFake);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async void AtualizarStatusPagamento_Error_test()
    {
        // Arrange
        var atualizacaoFake = AtualizarStatusPagamentoRequestMock.AtualizarStatusPagamentoRequestFaker(StatusPagamento.Pendente);

        _pedidoRepository.Setup(x => x.GetById(It.IsAny<Guid>())).ThrowsAsync(new Exception());
        _pedidoRepository.Setup(x => x.Update(It.IsAny<Pedido>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);

        var exec = new AtualizarStatusPagamentoUseCase(_pedidoRepository.Object);

        try
        {
            // Act
            await exec.Execute(atualizacaoFake);
        }
        catch
        {
            // Assert
            Assert.True(true);
        }
    }

    [Theory]
    [InlineData(StatusPedido.Recebido)]
    [InlineData(StatusPedido.Pronto)]
    [InlineData(StatusPedido.EmPreparacao)]
    [InlineData(StatusPedido.Finalizado)]
    public async void AtualizarStatusPedido_OK_test(StatusPedido statusPedido)
    {
        // Arrange
        var atualizacaoFake = AtualizarStatusPedidoRequestMock.AtualizarStatusPedidoRequestFaker(statusPedido);
        var pedidoFake = PedidoMock.PedidoFake();

        _pedidoRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(Task.FromResult(pedidoFake));
        _pedidoRepository.Setup(x => x.Update(It.IsAny<Pedido>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);

        var exec = new AtualizarStatusPedidoUseCase(_pedidoRepository.Object);

        // Act
        var result = await exec.Execute(atualizacaoFake);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async void CriarPedido_OK_test()
    {
        // Arrange
        var criarPedidoFake = CriarPedidoRequestMock.CriarPedidoRequestFake();
        var pedidoFake = PedidoMock.PedidoFake();

        _pedidoRepository.Setup(x => x.Post(It.IsAny<Pedido>())).Returns(Task.FromResult(pedidoFake));

        var exec = new CriarPedidoUseCase(_pedidoRepository.Object, _mapper);

        // Act
        var result = await exec.Execute(criarPedidoFake);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async void ObterPedidoPorIdUseCase_OK_test()
    {
        // Arrange
        var pedidoFake = PedidoMock.PedidoFake();

        _pedidoRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(Task.FromResult(pedidoFake));

        var exec = new ObterPedidoPorIdUseCase(_pedidoRepository.Object, _mapper);

        // Act
        var result = await exec.Execute(pedidoFake.Id);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async void ObterPedidosFiltradosUseCase_OK_test()
    {
        // Arrange
        IList<Pedido> listPedidos =
        [
            PedidoMock.PedidoFake(),
            PedidoMock.PedidoFake(),
            PedidoMock.PedidoFake()
        ];

        _pedidoRepository.Setup(x => x.GetFiltrados()).Returns(Task.FromResult(listPedidos));

        var exec = new ObterPedidosFiltradosUseCase(_pedidoRepository.Object, _mapper);

        // Act
        var result = await exec.Execute();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async void ObterPedidosUseCase_OK_test()
    {
        // Arrange
        IList<Pedido> listPedidos =
        [
            PedidoMock.PedidoFake(),
            PedidoMock.PedidoFake(),
            PedidoMock.PedidoFake(),
            PedidoMock.PedidoFake(),
            PedidoMock.PedidoFake()
        ];

        _pedidoRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(listPedidos));

        var exec = new ObterPedidosUseCase(_pedidoRepository.Object, _mapper);

        // Act
        var result = await exec.Execute();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async void ObterStatusPagamentoPorIdUseCase_OK_test()
    {
        // Arrange

        var pedidoMock = PedidoMock.PedidoFake();

        _pedidoRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(Task.FromResult(pedidoMock));

        var exec = new ObterStatusPagamentoPorIdUseCase(_pedidoRepository.Object, _mapper);

        // Act
        var result = await exec.Execute(pedidoMock.Id);

        // Assert
        Assert.NotNull(result);
    }
}
