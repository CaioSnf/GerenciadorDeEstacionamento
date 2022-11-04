using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Classes
{
    internal class Patio
    {
        public int QuantidadeVagasDisponiveis { get; set; }
        public decimal ValorHora { get; set; }  

        public decimal TotalFaturado { get; set; }

        public List<Vaga> Vagas { get; set; }

        public void ocuparVaga (Vaga vagaOcupada) {
            Vagas.Add(vagaOcupada);
            QuantidadeVagasDisponiveis -= 1;
        }
    }
    
}
