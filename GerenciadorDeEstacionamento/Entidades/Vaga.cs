using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Classes
{
    internal class Vaga
    {
        [Key]
        public int Id { get; set; }
        public Carro Carro { get; set; } 
        public DateTime HorarioEntrada { get; set; }

        public Vaga()// construtor da classe
        {
           
            HorarioEntrada = DateTime.Now;
        }
    }
}
