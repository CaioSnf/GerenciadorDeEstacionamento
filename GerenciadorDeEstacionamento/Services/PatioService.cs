using GerenciadorDeEstacionamento.Classes;
using GerenciadorDeEstacionamento.Data.Repositories;
using GerenciadorDeEstacionamento.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Services
{
    internal class PatioService
    {
        private readonly PatioRepository _patioRepository;
        private readonly CarroRepository _carroRepository;
        private readonly VagaRepository _vagaRepository;

        public PatioService(PatioRepository patioRepository, CarroRepository carroRepository, VagaRepository vagaRepository) 
        {
            _patioRepository = patioRepository;
            _carroRepository = carroRepository;
            _vagaRepository = vagaRepository;
        }
        
        public void MostrarPatios()
        {
            try
            {
                List<Patio> listaDePatio =  _patioRepository.RecuperarTodosOsPatios();
                if (listaDePatio == null)
                {
                    Console.WriteLine("Não existe nenhum patio cadastrado");
                    return;
                }
                foreach (Patio p in listaDePatio)
                {
                    Console.WriteLine($"Id: {p.Id}, Nome: {p.Nome}");

                }

            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                throw;
            }
           

        }
        public void BalizarCarro(bool entrada, string placa, int idPatio)
        {
            Carro carroEscolhido = _carroRepository.RetornarCarroPorPlaca(placa);
            Patio patio = _patioRepository.RecuperarPatioPorId(idPatio);
            if (entrada)
            {
                if (carroEscolhido != null && carroEscolhido.EstaEstacionado == false)
                {
                    carroEscolhido.EstaEstacionado = true;
                    _carroRepository.AtualizarCarro(carroEscolhido);
                    Vaga vagaOcupada = new Vaga();
                    vagaOcupada.Carro = carroEscolhido;
                    vagaOcupada.Patio = patio;
                    _vagaRepository.AdicionarVaga(vagaOcupada);
                    patio.QuantidadeVagasDisponiveis -= 1;
                    _patioRepository.AtualizarPatio(patio);
                    Console.WriteLine($"O carro com placa {vagaOcupada.Carro.Placa} foi estacionado");
                }
                else if (carroEscolhido != null && carroEscolhido.EstaEstacionado == true)
                {
                    Console.WriteLine($"O carro {carroEscolhido.Placa} ja esta estacionado");
                }
                else
                {
                    Console.WriteLine("O carro inserido nao existe");
                }
               
            }
            else
            {
                if (carroEscolhido != null && carroEscolhido.EstaEstacionado == true)
                {
                    carroEscolhido.EstaEstacionado = false;
                    _carroRepository.AtualizarCarro(carroEscolhido);
                    Vaga vaga = _vagaRepository.RecuperarVagaPorCarro(carroEscolhido);
                    decimal tarifa = Cobrar(vaga, patio);
                    _vagaRepository.RemoverVaga(vaga);
                    patio.QuantidadeVagasDisponiveis += 1;
                    _patioRepository.AtualizarPatio(patio);

                    Console.WriteLine($"O carro com a placa {carroEscolhido.Placa} saiu do patio e foi cobrado a tarifa de R$ {tarifa}");
                }
                else if (carroEscolhido != null && carroEscolhido.EstaEstacionado == false)
                {
                    Console.WriteLine($"O carro com a placa {carroEscolhido.Placa} não esta estacionado");
                }

                else
                {
                    Console.WriteLine("O carro inserido nao existe");
                }
            }
        }
        private decimal CalcularTarifa(int minutos,Patio patio, int segundos)
        {
            decimal tarifa = 0;

            if (minutos <= 60 && minutos >= 0 ) //1h
            {
                tarifa = patio.ValorHora * 1;
            }
            else if (minutos > 60 && minutos <= 120) // 2h
            {
                tarifa = patio.ValorHora * 2;
            }
            else if (minutos > 120 && minutos <= 180) // 3h
            {
                tarifa = patio.ValorHora * 3;
            }
            else if (minutos > 180 && minutos <= 240)// 4h
            {
                tarifa = patio.ValorHora * 4;
            }
            else
            {
                tarifa = patio.ValorDiaria;
            }
            return tarifa;  //return tem que retornar o tipo que especificamos ex:decimal, string

        }
        public decimal Cobrar(Vaga vaga,Patio patio)// void não pode ter return.
        {
            int minutos = (int)DateTime.Now.Subtract(vaga.HorarioEntrada).TotalMinutes;
            int segundos = (int)DateTime.Now.Subtract(vaga.HorarioEntrada).Seconds;
            decimal tarifa = CalcularTarifa(minutos,patio, segundos);
            patio.TotalFaturado += tarifa;
            _patioRepository.AtualizarPatio(patio);
            return tarifa;
        }
        public void CadastrarPatio()
        {
            Patio patio = new Patio();
            Console.Clear();
            Console.WriteLine("CADASTRO DE PATIO");
            Console.WriteLine("Qual o nome do Patio?");
            string nomePatio = Console.ReadLine()!;
            if (nomePatio == "")
            {
                Console.WriteLine("O nome informado é vazio");
                Console.ReadLine();
                Console.Clear();
                return;

            }
            patio.Nome = nomePatio!;

            Console.WriteLine("Qual valor da Hora?");
            string valorHora = Console.ReadLine()!;
            decimal valorHoraConvertido = 0;
            if (Decimal.TryParse(valorHora, out valorHoraConvertido))
            {
                patio.ValorHora = valorHoraConvertido;
            }
            else
            {
                Console.WriteLine("Valor inserido invalido");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Qual o valor da diaria?");
            string valorDiaria = Console.ReadLine()!;
            decimal valorDiariaConvertido = 0;
            if (Decimal.TryParse(valorDiaria, out valorDiariaConvertido))
            {
                patio.ValorDiaria = valorDiariaConvertido;
            }
            else
            {
                Console.WriteLine("Valor invalido inserido");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Qual a quantidade de vagas?");
            string quantidadeVagas = Console.ReadLine()!;
            int quantidadeVagasConvertido = 0;
            if (int.TryParse(quantidadeVagas, out quantidadeVagasConvertido))
            {
                patio.QuantidadeVagasDisponiveis = quantidadeVagasConvertido;
            }
            else
            {
                Console.WriteLine("Quantidade de vagas incoerente");
                Console.ReadLine();
                return;
            }

            _patioRepository.AdicionarPatio(patio);

            Console.Clear();



        }
        public void ObterTotalFaturado()
        {
            Console.Clear();
            Console.WriteLine("Qual Id do patio que deseja consultar?");
            string idPatio = Console.ReadLine()!;
            int idPatioInt = 0;
            if (int.TryParse(idPatio, out idPatioInt))
            {
                Patio patio = _patioRepository.RecuperarPatioPorId(idPatioInt);
                if (patio == null)
                {
                    Console.WriteLine($"O Id do patio fornecido é invalido");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine($@"O valor total foi R$ {patio.TotalFaturado}");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Caracter invalido");
                Console.ReadLine();
                Console.Clear();
                return;
            }
        }
        public void RetirarCarro()
        {
            List<Carro> carrosEstacionados = _carroRepository.RetornarCarrosEstacionados();
            
            Console.Clear();

            if (carrosEstacionados.Count != 0)
            {
                Console.WriteLine("Qual Id do patio que esta saindo o carro?");
                string idPatio = Console.ReadLine()!;
                int idPatioInt = 0;
                if (int.TryParse(idPatio, out idPatioInt))
                {
                    Patio patio = _patioRepository.RecuperarPatioPorId(idPatioInt);
                }
                else
                {
                    Console.WriteLine("Caracter invalido");
                    Console.ReadLine();
                    Console.Clear();
                    return;
                }
                Console.WriteLine("Qual carro esta saindo do patio?");
                MostrarCarros(true);

                string placaSaidaPatio = Console.ReadLine()!;
                BalizarCarro(false, placaSaidaPatio, idPatioInt);
            }
            else { Console.WriteLine("Não existe carro para ser retirado"); }
            Console.ReadLine();
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
        public void EstacionarCarro()
        {

            Console.Clear();
            List<Carro> carrosDisponiveis = _carroRepository.RetornarCarrosDisponiveis();
            if (carrosDisponiveis.Count != 0)
            {
                Console.WriteLine("Qual Id do patio que esta entrando o carro?");
                string idPatio = Console.ReadLine()!;
                int idPatioInt = 0;
                if (int.TryParse(idPatio, out idPatioInt))
                {
                    Patio patio = _patioRepository.RecuperarPatioPorId(idPatioInt);
                }
                else
                {
                    Console.WriteLine("Caracter invalido");
                    Console.ReadLine();
                    Console.Clear();
                    return;
                }
                Console.WriteLine("Escolha qual placa que entrou no patio");
                MostrarCarros(false);
                string placaEntradaPatio = Console.ReadLine()!;

                BalizarCarro(true, placaEntradaPatio, idPatioInt);
            }
            else
            {
                Console.WriteLine("Não existem carros disponiveis para estacionar");

            }
            Console.ReadLine();

            Console.Clear();
        }
        public void MostrarQuantidadeDeVagasDisponiveis()
        {
            Console.Clear();
            Console.WriteLine("Qual Id do patio que deseja consultar?");
            string idPatio = Console.ReadLine()!;
            int idPatioInt = 0;
            if (int.TryParse(idPatio, out idPatioInt))
            {
                Patio patio = _patioRepository.RecuperarPatioPorId(idPatioInt);
                Console.WriteLine($"Temos {patio.QuantidadeVagasDisponiveis} vagas disponíveis");
            }
            else
            {
                Console.WriteLine("Caracter invalido");
                Console.ReadLine();
                return;
            }

            Console.ReadLine();

            Console.Clear();
        }
        public void MostrarPatiosCadastrados()
        {
            Console.Clear();
            Console.WriteLine("Lista de Patios cadastrados");
            MostrarPatios();
            Console.ReadLine();
            Console.Clear();
        }
            


    }
}
