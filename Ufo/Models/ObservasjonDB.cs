using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observation.Models
{
    public class ObservasjonContext: DbContext                  // Our file calls "ObservasjonDB.cs", but we create ObservasjonContext-file
    {
        public ObservasjonContext(DbContextOptions<ObservasjonContext> options) : base(options)

        {
            Database.EnsureCreated();
        }

        public DbSet<Observasjon> Observasjoner { get; set; }
    }
}
