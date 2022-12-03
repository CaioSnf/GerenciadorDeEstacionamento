using GerenciadorDeEstacionamento.Classes;
using GerenciadorDeEstacionamento.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Data.Repositories
{
    internal class CarroRepository
    {
        private readonly AppDbContext _db;

        public CarroRepository(AppDbContext db)
        {
            _db = db;
        }
        public List<Carro> RetornarTodosOsCarros()
        {
            try
            {
                List<Carro> carrosDb = _db.Carros.ToList();
                return carrosDb;
            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                return new List<Carro>();
            }
        }
        public bool ExisteCarroComEstaPlaca(string placa) 
        {
            try
            {
                bool existeCarroComEstaPlaca =  _db.Carros.ToList().Any(carro => carro.Placa == placa);
                return existeCarroComEstaPlaca;
            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                throw;
            }
        }
        public void AdicionarCarro(Carro carro) 
        {
            try
            {
                _db.Carros.Add(carro);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                return;
            }
        }
        public void AtualizarCarro(Carro carro)
        {
            try
            {
                _db.Carros.Update(carro);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                throw;
            }
        }
        public List<Carro> RetornarCarrosEstacionados()
        {
            try
            {
                List<Carro> carros = RetornarTodosOsCarros();
                return carros.Where(carro => carro.EstaEstacionado == true).ToList();
            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                throw;
            }
        }
        public List<Carro> RetornarCarrosDisponiveis()
        {
            try
            {
                List<Carro> carros = RetornarTodosOsCarros();
                return carros.Where(carro => carro.EstaEstacionado == false).ToList();
            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                throw;
            }
        }
        public Carro RetornarCarroPorPlaca(string placa)
        {
            var carros = RetornarTodosOsCarros();
            return carros.FirstOrDefault(carro => carro.Placa == placa)!;
        }

    }
}
