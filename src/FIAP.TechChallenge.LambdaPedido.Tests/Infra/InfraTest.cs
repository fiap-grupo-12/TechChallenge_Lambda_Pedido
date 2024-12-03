using Amazon.DynamoDBv2.DataModel;
using Amazon.SQS;
using Amazon.SQS.Model;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;
using FIAP.TechChallenge.LambdaPedido.Infra.Data.Repositories;
using FIAP.TechChallenge.LambdaPedido.Infra.Message;
using FIAP.TechChallenge.LambdaPedido.Tests.Mock;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FIAP.TechChallenge.LambdaPedido.Tests.Infra
{
    public class InfraTest
    {
        private readonly Mock<IDynamoDBContext> mockContext;
        private readonly Mock<IAmazonSQS> mockSQS;
        private readonly PedidoRepository repository;
        private readonly MensageriaSolicitaPagamento mensageria;

        public InfraTest()
        {
            mockContext = new Mock<IDynamoDBContext>();
            mockSQS = new Mock<IAmazonSQS>();
            repository = new PedidoRepository(mockContext.Object);
            mensageria = new MensageriaSolicitaPagamento(mockSQS.Object);
        }

        [Fact]
        public async void GetAllTest()
        {
            var mock = PedidoMock.PedidosFake();

            mockContext.Setup
                (x => x.ScanAsync<Pedido>(It.IsAny<IEnumerable<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>()).GetRemainingAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(mock);

            var result = await repository.GetAll();

            Assert.True(mock.Equals(result));
        }

        [Fact]
        public async void GetByStatusTest()
        {
            var mock = PedidoMock.PedidosFake();

            mockContext.Setup
                (x => x.ScanAsync<Pedido>(It.IsAny<IEnumerable<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>()).GetRemainingAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(mock);

            var result = await repository.GetByStatus(StatusPedido.Recebido);

            Assert.True(mock.Equals(result));
        }

        [Fact]
        public async void GetByIdTest()
        {
            var mock = PedidoMock.PedidoFake();

            mockContext.Setup
                (x => x.LoadAsync<Pedido>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mock);

            var result = await repository.GetById(mock.Id);

            Assert.True(mock.Equals(result));
        }

        [Fact]
        public async void GetFiltradosTest()
        {
            var mock = PedidoMock.PedidosFake();

            mockContext.Setup
                (x => x.ScanAsync<Pedido>(It.IsAny<IEnumerable<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>()).GetRemainingAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(mock);

            var result = await repository.GetFiltrados();

            Assert.True(mock.Equals(result));
        }

        [Fact]
        public async void UpdateTest()
        {
            Pedido mock = PedidoMock.PedidoFake();

            mockContext.Setup
                (x => x.SaveAsync<Pedido>(mock, It.IsAny<DynamoDBOperationConfig>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await repository.Update(mock, mock.Id);
        }

        [Fact]
        public async void PostTest()
        {
            Pedido mock = PedidoMock.PedidoFake();

            mockContext.Setup
                (x => x.SaveAsync<Pedido>(mock, It.IsAny<DynamoDBOperationConfig>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await repository.Post(mock);
        }

        [Fact]
        public async void GetAllTestError()
        {
            mockContext.Setup(x => x.ScanAsync<Pedido>(It.IsAny<IEnumerable<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>()).GetRemainingAsync(It.IsAny<CancellationToken>()))
                       .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () => await repository.GetAll());
        }

        [Fact]
        public async void GetByStatusTestError()
        {
            mockContext.Setup
                (x => x.ScanAsync<Pedido>(It.IsAny<IEnumerable<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>()).GetRemainingAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () => await repository.GetByStatus(StatusPedido.Recebido));
        }

        [Fact]
        public async void GetByIdTestError()
        {
            var mock = PedidoMock.PedidoFake();
            mockContext.Setup
                (x => x.LoadAsync<Pedido>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () => await repository.GetById(mock.Id));
        }

        [Fact]
        public async void GetFiltradosTestError()
        {
            var mock = PedidoMock.PedidosFake();

            mockContext.Setup
                (x => x.ScanAsync<Pedido>(It.IsAny<IEnumerable<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>()).GetRemainingAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () => await repository.GetFiltrados());
        }

        [Fact]
        public async void UpdateTestError()
        {
            Pedido mock = PedidoMock.PedidoFake();

            mockContext.Setup
                (x => x.SaveAsync<Pedido>(mock, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () => await repository.Update(mock, mock.Id));
        }

        [Fact]
        public async void PostTestError()
        {
            Pedido mock = PedidoMock.PedidoFake();

            mockContext.Setup
                (x => x.SaveAsync<Pedido>(mock, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () => await repository.Post(mock));
        }

        [Fact]
        public async void PostMessage()
        {
            mockSQS.Setup
                (x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new SendMessageResponse()));

            await mensageria.SendMessage("{\"teste\":\"teste\"}");
        }
        [Fact]
        public async void PostMessageError()
        {
            mockSQS.Setup
                (x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () => await mensageria.SendMessage("{\"teste\":\"teste\"}"));
        }
    }
}
