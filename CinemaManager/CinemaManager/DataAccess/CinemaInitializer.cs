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
                new Theater() {CinemaId=1,TheaterNumber=1 },
                new Theater() {CinemaId=1,TheaterNumber=2 },
                new Theater() {CinemaId=1,TheaterNumber=3 }
            };

            theaters.ForEach(t => context.Theaters.Add(t));
            context.SaveChanges();

        }
    }
}
