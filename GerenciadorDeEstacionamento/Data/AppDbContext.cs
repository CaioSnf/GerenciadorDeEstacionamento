using GerenciadorDeEstacionamento.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEstacionamento.Data
{
    internal class AppDbContext : DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Initial Catalog=EstacionamentoDB; Data Source=localhost,1433; Persist Security Info=True; User ID=sa; Password=Caio123#;TrustServerCertificate=True;");

        }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Patio> Patios{ get; set; }
    }
}
