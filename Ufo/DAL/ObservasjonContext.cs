using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ufo.DAL
{
    public class Observasjoner
    {
        [Key]
        public int Id { get; set; }
        public string Dato { get; set; }
        public string Navn { get; set; }
        public string Tid { get; set; }
        public string Beskrivelse { get; set; }
        public string Lokasjon { get; set; }
    }

    public class Comments
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        virtual public Observasjoner Observations { get; set; }
    }

    public class ObservasjonContext : DbContext
    {
        public ObservasjonContext(DbContextOptions<ObservasjonContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Observasjoner> Observasjoner { get; set; }
        public DbSet<Comments> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
