using Entities.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Infra.Configuracao
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions options) : base(options)
        {            
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Despesa> Despesa{ get; set; }
        public DbSet<SistemaFinanceiro> SistemaFinanceiro { get; set; }
        public DbSet<UsuarioSistemaFinanceiro> UsuarioSistemaFinanceiro { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterSringConexão());
                base.OnConfiguring(optionsBuilder);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }

        public string ObterSringConexão()
        { 
        return "Data Source=DESKTOP-AREG4SF\\SQLEXPRESS;Inital Catalog=Financeiro_2024; Integrated Security=True";
        }
    }
}
