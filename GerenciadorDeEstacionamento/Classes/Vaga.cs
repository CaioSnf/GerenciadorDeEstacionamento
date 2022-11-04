using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Classes
{
    internal class Vaga
    {
        public Carro Carro { get; set; } 
        public DateTime HorarioEntrada { get; set; }
        public string Id { get; set; }

        public Vaga(Carro carro)
        {
            Carro = carro;
            HorarioEntrada = DateTime.Now;
        }
    }
}
