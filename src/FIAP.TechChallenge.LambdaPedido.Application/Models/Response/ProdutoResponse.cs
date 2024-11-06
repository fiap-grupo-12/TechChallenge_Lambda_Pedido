using FIAP.TechChallenge.LambdaPedido.Domain.Entities;

namespace FIAP.TechChallenge.LambdaPedido.Application.Models.Response
{
    public class ProdutoResponse
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public Categoria CategoriaProduto { get; set; }
    }
}
