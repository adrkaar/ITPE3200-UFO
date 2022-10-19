using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ufo.DAL
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ObservasjonContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var observasjon1 = new Observasjoner { Dato = "22.22.01", Navn = "er", Tid = "22.22", Beskrivelse = "Jeg gikk ut pub og så en stor egg i skyen.", Lokasjon = "Her" };
                var observasjon2 = new Observasjoner { Dato = "30.01.01", Navn = "er", Tid = "00.09", Beskrivelse = "Bla-bla-bla.", Lokasjon = "Der" };

                context.Observasjoner.Add(observasjon1);
                context.Observasjoner.Add(observasjon2);

                context.SaveChanges();
            }
        }
    }
}
