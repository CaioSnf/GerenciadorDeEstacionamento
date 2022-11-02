using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Classes
{
    internal class Carro
    {
        [RegularExpression(@"/[A-Z]{3}[0-9][0-9A-Z][0-9]{2}/", ErrorMessage = "Placa invalida")]
        public string Placa { get; set; }
        public string NomeProp { get; set; }
        public int Id { get; set; }

    }
}
