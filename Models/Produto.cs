using System.ComponentModel.DataAnnotations;
namespace WeChip.Models
{
    public class Produto
    {
        public Produto(long id, string descricao, float preco, string tipo, string codigo)
        {
            Id = id;
            Descricao = descricao;
            Preco = preco;
            Tipo = tipo;
            Codigo = codigo;
        }

        public long Id { get; set; }

        public string Descricao { get; set; }

        public float Preco { get; set; }

        public string Tipo { get; set; }
        
        public string Codigo { get; set; }
    }
}
