using GerenciadorDeEstacionamento.Classes;
using GerenciadorDeEstacionamento.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Services
{
    internal class CarroService
    {
        private readonly CarroRepository _carroRepository;
        private readonly VagaRepository _vagaRepository;

        public CarroService(CarroRepository carroRepository, 
                            VagaRepository vagaRepository)
        {
            _carroRepository = carroRepository;
            _vagaRepository = vagaRepository;
        }
        public void CadastrarCarro()
        {
            Console.Clear();

            Carro carro = new Carro();

            Console.WriteLine("Tela de cadastro de veículo");

            Console.WriteLine("Placa do carro");

            string placaDoCarro = Console.ReadLine()!;

            var existeCarroComEstaPlaca =  _carroRepository.ExisteCarroComEstaPlaca(placaDoCarro);

            if (existeCarroComEstaPlaca)
            {
                Console.WriteLine($"O carro com a placa {placaDoCarro} ja esta cadastrado");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            carro.Placa = placaDoCarro;

            Console.WriteLine("Proprietario");

            string nomeProprietario = Console.ReadLine()!;
            if(nomeProprietario == null || nomeProprietario == "")
            {
                Console.WriteLine($"O nome do Proprietario nao pode ser vazio!");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            carro.Proprietario = nomeProprietario;
            _carroRepository.AdicionarCarro(carro);
            Console.Clear();
        }
        public void MostrarCarros(bool estaEstacionado)
        {
            if (estaEstacionado)
            {
                List<Vaga> vagasDb = _vagaRepository.RecuperarTodasAsVagas();
                if (vagasDb == null)
                {
                    Console.WriteLine("Não existem carros estacionados");
                    Console.ReadLine();
                    Console.Clear();
                    return;
                }
                foreach (var vaga in vagasDb)
                {
                    TimeSpan tempoEstacionado = DateTime.Now.Subtract(vaga.HorarioEntrada);
                    Console.WriteLine($"Placa: {vaga.Carro.Placa} - Proprietario: {vaga.Carro.Proprietario} - Patio: {vaga.Patio.Nome} - TempoEstacionado: {(int)tempoEstacionado.TotalHours} horas, {(int)tempoEstacionado.Minutes} minutos e {(int)tempoEstacionado.Seconds} segundos!");
                }
            }
            else
            {
                List<Carro> carrosDb = _carroRepository.RetornarTodosOsCarros();
                if (carrosDb == null)
                {
                    Console.WriteLine("Não existem carros cadastrados");
                    Console.ReadLine();
                    Console.Clear();
                    return;
                }
                foreach (var carro in carrosDb)
                {
                    Console.WriteLine($"Placa: {carro.Placa} - Proprietario: {carro.Proprietario}");
                }
                return;
            }
        }
        public void MostrarCarrosDisponiveis()
        {
            Console.Clear();
            Console.WriteLine(" lista de veiculos");
            MostrarCarros(false);
            Console.ReadLine();
            Console.Clear();
        }
        public void MostrarCarrosEstacionados()
        {
            Console.Clear();
            MostrarCarros(true);
            Console.ReadLine();
            Console.Clear();
        }
    }
}
