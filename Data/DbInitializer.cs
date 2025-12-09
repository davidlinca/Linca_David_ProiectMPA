using Microsoft.EntityFrameworkCore;
using Linca_David_ProiectMPA.Models;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Linca_David_ProiectMPA.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Linca_David_ProiectMPAContext(
                serviceProvider.GetRequiredService<DbContextOptions<Linca_David_ProiectMPAContext>>()))
            {
                context.Database.EnsureCreated();

                if (context.Movie.Any())
                {
                    return;
                }

                var genres = new Genre[]
                {
                    new Genre { Name = "Science Fiction" },
                    new Genre { Name = "Thriller" },
                    new Genre { Name = "Comedy" },
                    new Genre { Name = "Documentary" }
                };
                context.Genre.AddRange(genres);

                var directors = new Director[]
                {
                    new Director { FullName = "Christopher Nolan", BirthDate = DateTime.Parse("1970-07-30") },
                    new Director { FullName = "Greta Gerwig", BirthDate = DateTime.Parse("1983-08-04") }
                };
                context.Director.AddRange(directors);

                var studios = new Studio[]
                {
                    new Studio { Name = "Warner Bros." },
                    new Studio { Name = "Netflix" }
                };
                context.Studio.AddRange(studios);

                var actors = new Actor[]
                {
                    new Actor { FirstName = "Ryan", LastName = "Gosling" },
                    new Actor { FirstName = "Cillian", LastName = "Murphy" },
                    new Actor { FirstName = "Margot", LastName = "Robbie" }
                };
                context.Actor.AddRange(actors);

                context.SaveChanges();


                var movies = new Movie[]
                {
                    new Movie
                    {
                        Title = "Oppenheimer",
                        Type = "Movie",
                        ReleaseYear = 2023,
                        Synopsis = "Thriller biografic epic care urmărește viața fizicianului teoretician american J. Robert Oppenheimer, „părintele bombei atomice”.",
                        GenreID = genres.Single(g => g.Name == "Thriller").ID,
                        DirectorID = directors.Single(d => d.FullName == "Christopher Nolan").ID,
                        StudioID = studios.Single(s => s.Name == "Warner Bros.").ID
                    },
                    new Movie
                    {
                        Title = "Barbie",
                        Type = "Movie",
                        ReleaseYear = 2023,
                        Synopsis = "Urmărește povestea lui Barbie (Margot Robbie) pe măsură ce viața ei perfectă și matriarhală din Barbie Land se destramă, ceea ce o poartă pe ea și pe Ken (Ryan Gosling) într-o călătorie către lumea reală.",
                        GenreID = genres.Single(g => g.Name == "Comedy").ID,
                        DirectorID = directors.Single(d => d.FullName == "Greta Gerwig").ID,
                        StudioID = studios.Single(s => s.Name == "Warner Bros.").ID
                    }
                };
                context.Movie.AddRange(movies);

                context.SaveChanges();
            }
        }
    }
}