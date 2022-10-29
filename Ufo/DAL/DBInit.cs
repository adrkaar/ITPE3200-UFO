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
                var observation1 = new Observations { Date = "2022-09-21", Time = "22:22", Description = "I went outside and saw a big egg in the sky", Latitude = "56.660151627670686", Longitude = "14.07792153985528", UfoTypes = type1 };
                var observation2 = new Observations { Date = "2022-10-01", Time = "00:09", Description = "I saw an UFO!!", Latitude = "48.647983479154824", Longitude = "9.865054057063944", UfoTypes = type3 };
                var observation3 = new Observations { Date = "2022-10-13", Time = "23:09", Latitude = "69.955420", Longitude = "23.139056", UfoTypes = type4, Description = "I saw Swirling rivers of greenish-blue light in the sky, obviously from an UFO" };
                var observation4 = new Observations { Date = "2022-10-13", Time = "23:42", Latitude = "70.121383", Longitude = "25.403750", UfoTypes = type4, Description = "I have never seen anything like this before, the whole sky lit up in an alien green color" };
                var observation5 = new Observations { Date = "2022-10-13", Time = "23:11", Latitude = "70.173617", Longitude = "24.876054", UfoTypes = type4, Description = "There was an alien rave light show" };
                var observation6 = new Observations { Date = "2022-10-13", Time = "23:27", Latitude = "69.729211", Longitude = "23.051106", UfoTypes = type4, Description = "A big green dancing cloud appeared in the sky", };
                var observation7 = new Observations { Date = "2022-10-14", Time = "00:53", Latitude = "70.586765", Longitude = "24.898042", UfoTypes = type4, Description = "The sky lighted up in the middle of the night, how weird" };

                context.Observations.Add(observation1);
                context.Observations.Add(observation2);
                context.Observations.Add(observation3);
                context.Observations.Add(observation4);
                context.Observations.Add(observation5);
                context.Observations.Add(observation6);
                context.Observations.Add(observation7);

                /* Comments */
                var comment1 = new Comments { Text = "Cool!", Observations = observation1, UpVote = 2, Downvote = 1 };
                var comment2 = new Comments { Text = "No my friend, ufos are not real", Observations = observation1, Downvote = 9 };
                var comment3 = new Comments { Text = "Wow, such a cool thing", Observations = observation2 };
                var comment4 = new Comments { Text = "Are you sure this was not northern lights??", Observations = observation3 };

                context.Comments.Add(comment1);
                context.Comments.Add(comment2);
                context.Comments.Add(comment3);
                context.Comments.Add(comment4);

                context.SaveChanges();
            }
        }
    }
}
