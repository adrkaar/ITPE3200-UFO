using Microsoft.EntityFrameworkCore;

namespace Ufo.DAL
{
    public class Observasjoner
    {
        public int Id { get; set; }
        public string Dato { get; set; }
        public string Navn { get; set; }
        public string Tid { get; set; }
        public string Beskrivelse { get; set; }
        public string Lokasjon { get; set; }
    }

    public class ObservasjonContext : DbContext
    {
        public ObservasjonContext(DbContextOptions<ObservasjonContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Observasjoner> Observasjoner { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
