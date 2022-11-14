﻿using Microsoft.AspNetCore.Builder;
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
                var observation1 = new Observations { Date = "2022-09-21", Time = "22:22", Latitude = "56.66015", Longitude = "14.07792", UfoTypes = type1, Description = "I went outside and saw a big egg in the sky" };
                var observation2 = new Observations { Date = "2022-10-01", Time = "00:09", Latitude = "48.64798", Longitude = "9.86505", UfoTypes = type3, Description = "I saw an UFO!!" };
                var observation3 = new Observations { Date = "2022-10-13", Time = "23:09", Latitude = "69.955420", Longitude = "23.139056", UfoTypes = type4, Description = "I saw Swirling rivers of greenish-blue light in the sky, obviously from an UFO" };
                var observation4 = new Observations { Date = "2022-10-13", Time = "23:42", Latitude = "70.121383", Longitude = "25.403750", UfoTypes = type4, Description = "I have never seen anything like this before, the whole sky lit up in an alien green color" };
                var observation5 = new Observations { Date = "2022-10-13", Time = "23:11", Latitude = "70.173617", Longitude = "24.876054", UfoTypes = type4, Description = "There was an alien rave light show" };
                var observation6 = new Observations { Date = "2022-10-13", Time = "23:27", Latitude = "69.729211", Longitude = "23.051106", UfoTypes = type4, Description = "A big green dancing cloud appeared in the sky", };
                var observation7 = new Observations { Date = "2022-10-14", Time = "00:53", Latitude = "70.586765", Longitude = "24.898042", UfoTypes = type4, Description = "The sky lighted up in the middle of the night, how weird" };
                var observation8 = new Observations { Date = "2022-09-21", Time = "21:53", Latitude = "38.081249", Longitude = "-105.32241", UfoTypes = type2, Description = "I saw some strange things in the sky one day. They looked like circular lights, they seemed to pass behind the clouds and run from west to east. They were passing so rapidly that it seemed to circle the earth again and again, because these strange objects repeatedly passed from west to east, being seen to leave the other side." };
                var observation9 = new Observations { Date = "2022-08-14", Time = "22:13", Latitude = "43.765806", Longitude = "-98.720334", UfoTypes = type1, Description = "Ive been seeing these for a few months now. My wife and eldest son as well. I wasn’t sure I could actually explain away all the other alternatives it could be so Ive not been confident it was real “UFOs”." };
                var observation10 = new Observations { Date = "2022-08-12", Time = "22:27", Latitude = "42.741635", Longitude = "-107.33936", UfoTypes = type3, Description = "We were all sat outside late at night when three big bright lights, much larger than any normal star, forming a perfect triangle came flying over from a North-Easterly direction." };
                var observation11 = new Observations { Date = "2022-10-16", Time = "20:16", Latitude = "34.373669", Longitude = "-110.32964", UfoTypes = type1, Description = "I was sitting on the porch.  My neighbor, pointed to the sky. She kept saying come here. I stepped off the porch. Sure enough, I counted 7 smaller orbs. Floating up and down. The top had a white glow." };
                var observation12 = new Observations { Date = "2022-09-04", Time = "03:00", Latitude = "32.169414", Longitude = "-95.73057", UfoTypes = type1, Description = "I saw one in the woods one night, looks like a blue ball of energy far away, poofing in and out of air like it has camo or something" };

                context.Observations.Add(observation1);
                context.Observations.Add(observation2);
                context.Observations.Add(observation3);
                context.Observations.Add(observation4);
                context.Observations.Add(observation5);
                context.Observations.Add(observation6);
                context.Observations.Add(observation7);
                context.Observations.Add(observation8);
                context.Observations.Add(observation9);
                context.Observations.Add(observation10);
                context.Observations.Add(observation11);
                context.Observations.Add(observation12);

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
