using GerenciadorDeEstacionamento.Classes;
using GerenciadorDeEstacionamento.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Data.Repositories
{
    internal class VagaRepository
    {
        private readonly AppDbContext _db;
        public VagaRepository(AppDbContext db)
        {
            _db = db;
        
        }

        public List<Vaga> RecuperarTodasAsVagas()
        {
            try
            {
                return _db.Vagas.Include(vaga => vaga.Patio).Include(vaga => vaga.Carro).ToList();
            }
            catch (Exception ex)
            {
               Util.MostrarErroNaTela(ex);
                throw;
            }
            
        }
        public List<Vaga> RecuperarVagasPorPatio(int IdPatio)
        {
            try
            {
                List<Vaga> Vagas = RecuperarTodasAsVagas();
                return Vagas.Where(vaga => vaga.Patio.Id == IdPatio).ToList();
            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                throw;
            }
        }
        public void AdicionarVaga(Vaga vaga)
        {
            try
            {
                _db.Vagas.Add(vaga);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                throw;
            }
        }
        public Vaga RecuperarVagaPorCarro(Carro carro)
        {
            try
            {
                List<Vaga> vagas = RecuperarTodasAsVagas();
                return vagas.FirstOrDefault(vaga => vaga.Carro == carro)!;
            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                throw;
            }
        }
        public void RemoverVaga(Vaga vaga)
        {
            try
            {
                _db.Vagas.Remove(vaga);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                throw;
            }
        }
    }
}
