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
                var context = serviceScope.ServiceProvider.GetService<ObservationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                /* Ufo types */
                var type1 = new UfoTypes { Type = "Round" };
                var type2 = new UfoTypes { Type = "Flat" };
                var type3 = new UfoTypes { Type = "Big" };
                var type4 = new UfoTypes { Type = "Invisible" };

                context.UfoTypes.Add(type1);
                context.UfoTypes.Add(type2);
                context.UfoTypes.Add(type3);
                context.UfoTypes.Add(type4);

                /* Observations */
                var observation1 = new Observations { Date = "2022-09-21", Time = "22:22", Description = "Jeg gikk ut pub og så en stor egg i skyen.", Latitude = "56.660151627670686", Longitude = "14.07792153985528", UfoTypes = type1 };
                var observation2 = new Observations { Date = "2022-10-01", Time = "00:09", Description = "Bla-bla-bla.", Latitude = "48.647983479154824", Longitude = "9.865054057063944", UfoTypes = type3 };

                context.Observations.Add(observation1);
                context.Observations.Add(observation2);

                /* Comments */
                var comment1 = new Comments { Text = "Cool!", Observations = observation1, UpVote = 2, Downvote = 1 };
                var comment2 = new Comments { Text = "No my friend, ufos are not real", Observations = observation1, Downvote = 9 };
                var comment3 = new Comments { Text = "Wow, such a cool thing", Observations = observation2 };

                context.Comments.Add(comment1);
                context.Comments.Add(comment2);
                context.Comments.Add(comment3);

                context.SaveChanges();
            }
        }
    }
}
