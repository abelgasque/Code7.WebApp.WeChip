using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeChip.Models
{
    public class Cliente
    {
        public Cliente(long id, string nome, string cpf, float credito, string telefone, long idStatus, long idEndereco)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Credito = credito;
            Telefone = telefone;
            IdStatus = idStatus;
            IdEndereco = idEndereco;
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatorio!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatorio!")]
        public string Cpf { get; set; }

        public float Credito { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatorio!")]
        public string Telefone { get; set; }

        public long IdStatus { get; set; }
        
        public long IdEndereco { get; set; }

        public Status Status { get; set; }

        public Endereco Endereco { get; set; }
    }
}
