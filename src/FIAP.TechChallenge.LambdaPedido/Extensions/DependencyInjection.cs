using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases;
using FIAP.TechChallenge.LambdaPedido.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaPedido.Domain.Repositories;
using FIAP.TechChallenge.LambdaPedido.Infra.Data.Configurations;
using FIAP.TechChallenge.LambdaPedido.Infra.Data.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaPedido.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static void AddProjectDependencies(this IServiceCollection services)
        {
            
            //AWS
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddTransient<IDynamoDBContext, DynamoDBContext>();

            //AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperConfig());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Repository
            services.AddTransient<IPedidoRepository, PedidoRepository>();

            //UseCase
            services.AddTransient<ICriarPedidoUseCase, CriarPedidoUseCase>();
            services.AddTransient<IAtualizarStatusPedidoUseCase, AtualizarStatusPedidoUseCase>();
            services.AddTransient<IObterPedidoPorIdUseCase, ObterPedidoPorIdUseCase>();
            services.AddTransient<IObterPedidosUseCase, ObterPedidosUseCase>();
            services.AddTransient<IObterPedidosFiltradosUseCase, ObterPedidosFiltradosUseCase>();
            services.AddTransient<IObterStatusPagamentoPorIdUseCase, ObterStatusPagamentoPorIdUseCase>();
            services.AddTransient<IAtualizarStatusPagamentoUseCase, AtualizarStatusPagamentoUseCase>();
        }
    }
}
