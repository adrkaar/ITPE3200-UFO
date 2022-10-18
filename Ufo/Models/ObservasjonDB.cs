using Microsoft.EntityFrameworkCore;

namespace Ufo.Models                                        // In relasjonsdatabaser - det er som KundeContekst.cs
{
    public class Observasjoner
    {
        public int Id { get; set; }                                     // to copy all PK for Observasjon
        public string Dato { get; set; }
        public string Navn { get; set; }
        public string Tid { get; set; }
        public string Beskrivelse { get; set; }
        public string Lokasjon { get; set; }
        // virtual public Ufoer UFO { get; set; }

        /*public static implicit operator Observasjoner(Observasjon v)
        {
            throw new NotImplementedException();
        }*/
    }


    /*public class Ufoer
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(DatabaseGeneratedOption.None)]         
        public string IdUfo { get; set; }
        public string NavnUfo { get; set; }
        public string TypeUfo { get; set; }
        public string BeskrivelseUfo { get; set; }

        /* public static implicit operator Ufoer(UFO v)
        {
            throw new NotImplementedException();
        } */
    /* }
 */
    public class ObservasjonContext : DbContext                  // Our file calls "ObservasjonDB.cs", but we create ObservasjonContext
    {
        public ObservasjonContext(DbContextOptions<ObservasjonContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Observasjoner> Observasjoner { get; set; }
        //public DbSet<UFO> Ufoer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // må importere pakken Microsoft.EntityFrameworkCore.Proxies
            // og legge til"viritual" på de attriuttene som ønskes å lastes automatisk (LazyLoading)
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
