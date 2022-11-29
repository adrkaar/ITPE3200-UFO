using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ufo.DAL
{
    [ExcludeFromCodeCoverage]
    public class Observations
    {
        [Key]
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        virtual public UfoTypes UfoTypes { get; set; }
        virtual public Users Users { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        virtual public Observations Observations { get; set; }
        public int UpVote { get; set; }
        public int Downvote { get; set; }
        virtual public Users Users { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class UfoTypes
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ObservationContext : DbContext
    {
        public ObservationContext(DbContextOptions<ObservationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Observations> Observations { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<UfoTypes> UfoTypes { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}