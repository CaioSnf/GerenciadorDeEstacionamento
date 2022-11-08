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
        public void desocuparVaga(Carro carro)
        {
            Vaga vagaOcupada = new Vaga(carro);
            vagaOcupada = Vagas.FirstOrDefault(vaga => vaga.Carro == carro);
            Vagas.Remove(vagaOcupada);
            QuantidadeVagasDisponiveis += 1;
        }
        public void cobrar(Vaga vaga)
        {
            int minutos = (int)DateTime.Now.Subtract(vaga.HorarioEntrada).TotalMinutes;
            
           
            
        }


    }
    
}
