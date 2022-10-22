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

                /* Observations */
                var observasjon1 = new Observations { Date = "22.22.01", Name = "er", Time = "22.22", Description = "Jeg gikk ut pub og så en stor egg i skyen.", Location = "Her" };
                var observasjon2 = new Observations { Date = "30.01.01", Name = "er", Time = "00.09", Description = "Bla-bla-bla.", Location = "Der" };

                /* Comments */
                var comment1 = new Comments { Text = "Cool1", Observations = observasjon1 };
                var comment2 = new Comments { Text = "No my friend, ufos are not real", Observations = observasjon1 };
                var comment3 = new Comments { Text = "Wow, such a cool thing", Observations = observasjon2 };

                context.Observations.Add(observasjon1);
                context.Observations.Add(observasjon2);
                context.Comments.Add(comment1);
                context.Comments.Add(comment2);
                context.Comments.Add(comment3);

                context.SaveChanges();
            }
        }
    }
}
