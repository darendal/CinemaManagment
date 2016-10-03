using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager.DataAccess
{
    public class CinemaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CinemaContext>
    {
        protected override void Seed(CinemaContext context)
        {
            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Title = "It Came From Space!",
                    Rating = Rating.PG13,
                },

                new Movie()
                {
                    Title = "It Back Came From Space! The Re-Spacening",
                    Rating = Rating.PG13,
                }

            };
            movies.ForEach(m => context.Movies.Add(m));
            context.SaveChanges();

            var cinemas = new List<Cinema>()
            {
                new Cinema() {Name="Cinema1", Address="123 Fake Street" }

            };
            cinemas.ForEach(c => context.Cinemas.Add(c));
            context.SaveChanges();

            var movieLeases = new List<MovieLease>()
            {
                new MovieLease()
                {
                    MovieId = 1,
                    CinemaId = 1
                }
            };
            movieLeases.ForEach(l => context.MovieLeases.Add(l));
            context.SaveChanges();


            var theaters = new List<Theater>()
            {
                new Theater() {CinemaId=1 },
                new Theater() {CinemaId=2 },
                new Theater() {CinemaId=3 }
            };

            theaters.ForEach(t => context.Theaters.Add(t));
            context.SaveChanges();

        }
    }
}
