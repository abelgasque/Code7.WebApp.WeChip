using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeChip.Models
{
    public class Oferta
    {
        public Oferta(long id, long idProduto, long idStatus, long idCliente)
        {
            Id = id;
            IdProduto = idProduto;
            IdStatus = idStatus;
            IdCliente = idCliente;
        }

        public long Id { get; set; }

        public long IdProduto { get; set; }

        public long IdStatus { get; set; }

        public long IdCliente { get; set; }

        public Produto Produto { get; set; }

        public Status Status { get; set; }

        public Cliente Cliente { get; set; }

    }
}
