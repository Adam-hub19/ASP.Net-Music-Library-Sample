using MusicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibrary.Data
{
    public class InitialData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any artists.
            if (context.Artists.Any())
            {
                return;   
            }

            var artists = new Artist[]
            {
                new Artist { Name = "Abdinasir"},
                new Artist { Name = "Shakur"},
            };

            foreach ( Artist a in artists)
            {

                context.Artists.Add(a);
            }
            context.SaveChanges();

            // Album

            var albums = new Album[]
           {
                new Album { Title = "The first Album"},
                new Album { Title = "The second album"},
           };

            foreach (Album al in albums)
            {

                context.Albums.Add(al);
            }
            context.SaveChanges();

            // Genre

            var genres = new Genre[]
        {
                new Genre { Name = "The first Genre"},
                new Genre { Name = "The second Genre"},
        };

            foreach (Genre g in genres)
            {

                context.Genres.Add(g);
            }
            context.SaveChanges();







        }
    }
}
