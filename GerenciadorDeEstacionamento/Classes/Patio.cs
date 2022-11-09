using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Classes
{
    internal class Patio
    {
        public decimal ValorDiaria { get; set; }

        public int QuantidadeVagasDisponiveis { get; set; }
        public decimal ValorHora { get; set; }  

        public decimal TotalFaturado { get; set; }

        public List<Vaga> Vagas { get; set; }


        public Patio()
        {
            ValorDiaria = 20M;

        }

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
            cobrar(vagaOcupada);
        }
        public void cobrar(Vaga vaga)
        {
            int minutos = (int)DateTime.Now.Subtract(vaga.HorarioEntrada).TotalMinutes;
            if (minutos <= 60 && minutos >0 ) //1h
            {
                TotalFaturado += ValorHora * 1;
            }
            else if (minutos >60 && minutos <= 120) // 2h
            {
                TotalFaturado += ValorHora * 2;
            }
            else if (minutos >120 && minutos <= 180) // 3h
            {
                TotalFaturado += ValorHora * 3;
            }
            else if (minutos >180 && minutos <= 240)// 4h
            {
                TotalFaturado += ValorHora *4;    


            }
            else {
                TotalFaturado += ValorDiaria;
            }
            
            

           
            
        }


    }
    
}
