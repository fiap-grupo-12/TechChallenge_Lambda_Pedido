using AutoMapper;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Request;
using FIAP.TechChallenge.LambdaPedido.Application.Models.Response;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities;
using FIAP.TechChallenge.LambdaPedido.Domain.Entities.Enum;

namespace FIAP.TechChallenge.LambdaPedido.Infra.Data.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //Request
            CreateMap<ClienteRequest, Cliente>().ReverseMap();
            CreateMap<FormaPagamentoRequest, FormaPagamento>().ReverseMap();
            CreateMap<ItemDePedidoRequest, ItemDePedido>().ReverseMap();

            //Response
            CreateMap<ClienteResponse, Cliente>().ReverseMap();
            CreateMap<FormaPagamentoResponse, FormaPagamento>().ReverseMap();
            CreateMap<Pedido, PedidoResponse>().ReverseMap();
            CreateMap<Cliente, ClienteResponse>().ReverseMap();
            CreateMap<FormaPagamento, FormaPagamentoResponse>().ReverseMap();
            CreateMap<ItemDePedido, ItensDePedidoResponse>().ReverseMap();
           

            CreateMap<Pedido, StatusPagamentoResponse>()
                .ForMember(dest => dest.StatusPagamento, opt => opt.MapFrom(src => src.StatusPagamento.GetDescription()))
                .ReverseMap();
        }
    }
}
