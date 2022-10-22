﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ufo.DAL
{
    public class Observations
    {
        [Key]
        public int Id { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        virtual public UfoTypes UfoTypes { get; set; }

    }

    public class Comments
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        virtual public Observations Observations { get; set; }
        public int UpVote { get; set; }
        public int Downvote { get; set; }
    }

    public class UfoTypes
    {
        public int Id { get; set; }
        public string TypeUfo { get; set; }
    }

    public class ObservasjonContext : DbContext
    {
        public ObservasjonContext(DbContextOptions<ObservasjonContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Observations> Observations { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<UfoTypes> UfoTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
