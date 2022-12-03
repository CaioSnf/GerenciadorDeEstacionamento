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
    internal class PatioRepository
    {
        private readonly AppDbContext _db;
        public PatioRepository(AppDbContext db)
        {
            _db = db;
        }
        public Patio RecuperarPatioPorId(int idPatio)
        {
            try
            {
                List<Patio> patios = RecuperarTodosOsPatios();
                return patios.FirstOrDefault(patio => patio.Id == idPatio)!;
            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                throw;
            }
        }
        public List<Patio> RecuperarTodosOsPatios()
        {
            try
            {
                var patios = _db.Patios.ToList();
                return patios;
            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                return new List<Patio>();
            }
        }
        public void AtualizarPatio(Patio patio)
        {
            try
            {
                _db.Patios.Update(patio);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
               
            }
        }
        public void AdicionarPatio(Patio patio)
        {
            try
            {
                _db.Patios.Add(patio);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                Util.MostrarErroNaTela(ex);
                
            }
        }
        
    }
}
