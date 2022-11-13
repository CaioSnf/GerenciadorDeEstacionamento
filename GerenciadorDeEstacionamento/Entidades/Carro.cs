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
        [Key]
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Proprietario { get; set; }

        public bool EstaEstacionado { get; set; }

        public Carro()// o que estiver entre as chaves do construtor sera executado quando criar um objeto da classe
        {
            EstaEstacionado = false;   
        } 
    }
}
