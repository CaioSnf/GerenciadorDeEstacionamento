using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Classes
{
    internal class Patio
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }    
        public decimal ValorDiaria { get; set; }

        public int QuantidadeVagasDisponiveis { get; set; }
        public decimal ValorHora { get; set; }

        public decimal TotalFaturado { get; set; }




        public Patio()
        {


        }

      


    }

}
