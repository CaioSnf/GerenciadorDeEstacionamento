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

        public decimal calcularTarifa(int minutos)
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




        public decimal cobrar(Vaga vaga)// void não pode ter return.
        {
            int minutos = (int)DateTime.Now.Subtract(vaga.HorarioEntrada).TotalMinutes;
            decimal tarifa = calcularTarifa(minutos);
            TotalFaturado += tarifa;
            return tarifa;
        }


    }

}
