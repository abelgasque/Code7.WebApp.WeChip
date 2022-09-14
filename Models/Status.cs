using System;
using System.ComponentModel.DataAnnotations;

namespace WeChip.Models
{
    public class Status
    {
        

        public long Id { get; set; }

        public string Descricao { get; set; }

        public Boolean FinalizaCliente { get; set; }

        public Boolean ContabilizarVenda { get; set; }

        public string Codigo { get; set; }

        public long IdCliente { get; set; }
    }
}
