using GerenciadorDeEstacionamento.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Utils
{
    internal class Util
    {
        private readonly PatioService _patioService;
        private readonly CarroService _carroService;
        public Util(PatioService patioService, CarroService carroService)
        {
            _patioService = patioService;
            _carroService = carroService;  
        }

        public static void MostrarErroNaTela(Exception ex) 
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            Console.ReadLine();
            Console.Clear();
        }
        public void MostrarCadastros()
        {
            Console.Clear();
            Console.WriteLine("Escolha qual item deseja cadastrar");
            Console.WriteLine("1 - Patio");
            Console.WriteLine("2 - Carro ");
            Console.WriteLine("0 - Voltar");
            string itemEscolhido = Console.ReadLine()!;


            switch (itemEscolhido)
            {
                case "1":
                    _patioService.CadastrarPatio();
                    break;
                case "2":
                     _carroService.CadastrarCarro();
                    break;

                default:
                    break;

            }
        }
        public static string MontarMenu()
        {
            Console.WriteLine("Bem vindo ao estacionamento Ki Vaga");
            Console.WriteLine("1- Cadastros");
            Console.WriteLine("2- Entrada de veículo no pátio");
            Console.WriteLine("3- Saida do veículo do pátio");
            Console.WriteLine("4- Quantidade de vagas disponiveis");
            Console.WriteLine("5- Total faturado");
            Console.WriteLine("6- Mostrar veiculos estacionados");
            Console.WriteLine("7- Carros cadastrados");
            Console.WriteLine("8- Lista de Patios");
            Console.WriteLine("0- Fechar programa");


            string escolhaMenu = Console.ReadLine()!;
            return escolhaMenu;
        }
    }
}
