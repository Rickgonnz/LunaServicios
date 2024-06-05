using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MantenedorApiContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }

        public MantenedorApiContext(DbContextOptions<MantenedorApiContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Clientes");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ClienteId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
