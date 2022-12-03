using GerenciadorDeEstacionamento.Classes;
using GerenciadorDeEstacionamento.Data;
using GerenciadorDeEstacionamento.Data.Repositories;
using GerenciadorDeEstacionamento.Services;
using GerenciadorDeEstacionamento.Utils;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

AppDbContext db = new AppDbContext();//contexto do banco
PatioRepository patioRepository = new PatioRepository(db);
VagaRepository vagaRepository = new VagaRepository(db);
CarroRepository carroRepository = new CarroRepository(db);
PatioService _patioService = new PatioService(patioRepository,carroRepository,vagaRepository);
CarroService _carroService = new CarroService(carroRepository, vagaRepository);
Util util = new Util(_patioService,_carroService);
//variaveis - tipo nome = valor inicial
string escolhaMenu = "";
//While mantem o programa aberto enquanto o usuario nao escolhe a opçao 0
async void menu() 
{
    while (escolhaMenu != "0")
    {
        escolhaMenu = Util.MontarMenu();

        switch (escolhaMenu)
        {
            case "1":
                util.MostrarCadastros();
                break;

            case "2":
                _patioService.EstacionarCarro();
                break;

            case "3":
                _patioService.RetirarCarro();
                break;

            case "4":
                _patioService.MostrarQuantidadeDeVagasDisponiveis();
                break;

            case "5":
                _patioService.ObterTotalFaturado();
                break;

            case "6":
                _carroService.MostrarCarrosEstacionados();
                break;

            case "7":
                _carroService.MostrarCarrosDisponiveis();
                break;

            case "8":
                 _patioService.MostrarPatiosCadastrados();
                break;


            case "0":
                break;

            default:
                Console.WriteLine("Escolheu a opção errada,volta otario");

                break;


        }
    }
}
menu();