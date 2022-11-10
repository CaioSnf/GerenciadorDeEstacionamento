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

        public decimal calcularTarifa (int minutos) 
        { 
            decimal tarifa = 0;

            if (minutos <= 60 && minutos > 0) //1h
            {
                tarifa = ValorHora * 1;
            }
            else if (minutos > 60 && minutos <= 120) // 2h
            {
                tarifa = ValorHora * 2;
            }
            else if (minutos > 120 && minutos <= 180) // 3h
            {
                tarifa = ValorHora * 3;
            }
            else if (minutos > 180 && minutos <= 240)// 4h
            {
                tarifa = ValorHora * 4;
            }
            else
            {
                tarifa = ValorDiaria;
            }
            return tarifa;  //return tem que retornar o tipo que especificamos ex:decimal, string

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
        }
        public decimal cobrar(Carro carro)// void não pode ter return.
        {
            
            Vaga vaga = new Vaga(carro);
            vaga = Vagas.FirstOrDefault(vaga => vaga.Carro == carro);

            int minutos = (int)DateTime.Now.Subtract(vaga.HorarioEntrada).TotalMinutes;
            decimal tarifa = calcularTarifa(minutos);
            TotalFaturado += tarifa;
            return tarifa;
        }


    }
    
}
