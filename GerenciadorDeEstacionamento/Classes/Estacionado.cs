using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Classes
{
    internal class Estacionado
    {
        public Carro Carro { get; set; } 
        public DateTime HorarioEntrada { get; set; }
        public string Id { get; set; }

    }
}
