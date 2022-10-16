using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observation.Models
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ObservasjonContext>();

                // må slette og opprette databasen hver gang når den skalinitieres (seed`es)
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var ufo1 = new Ufoer { NavnUfo = "Oslo Egg" , TypeUfo = "Egg", BeskrivelseUfo = "Som Påske egg, men stor og kan bli rød eller blå."};
                var ufo2 = new Ufoer { NavnUfo = "Barselona spagetti", TypeUfo = "En stikk", BeskrivelseUfo = "En linje, som beveger seg."};

                var observasjon1 = new Observasjoner { Dato = "22.22.01", Tid = "22.22", Beskrivelse = "Jeg gikk ut pub og så en stor egg i skyen.", UFO = ufo1 };
                var observasjon2 = new Observasjoner { Dato = "30.01.01", Tid = "00.09", Beskrivelse = "Bla-bla-bla.", UFO = ufo2 };

                context.Observasjoner.Add(observasjon1);
                context.Observasjoner.Add(observasjon2);

                context.SaveChanges();
            }
        }
    }
}
