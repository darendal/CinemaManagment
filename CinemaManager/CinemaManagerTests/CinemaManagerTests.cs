using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CinemaManager;
using CinemaManager.DataAccess;
using System.Linq;
using System.Collections.Generic;
using CinemaManagerWeb.Controllers;
using CinemaManagerWeb.Models;
using System.Threading.Tasks;
using CinemaManagerWeb.Models.DTO;
using System.Web.Http.Results;
using CinemaManagerWeb.Providers;

namespace CinemaManagerTests
{
    [TestClass]
    public class CinemaManagerTests
    {

        Cinema goodCinema = new Cinema()
        {
            Name = "UnitTestCinema",
            Address = "345 Fake Street",
            CloseTime = new TimeSpan(23, 59, 59),
            OpenTime = new TimeSpan(0, 0, 0),
            Theaters = new List<Theater>()
        };

        Cinema goodCinemaWithTheaters = new Cinema()
        {
            Name = "UnitTestCinema",
            Address = "345 Fake Street",
            CloseTime = new TimeSpan(23, 59, 59),
            OpenTime = new TimeSpan(0, 0, 0),
            Theaters = new List<Theater>()
            {
                new Theater() {TheaterNumber = 1 },
                new Theater() {TheaterNumber = 2 },
                new Theater() {TheaterNumber = 3 }
            }
        };

        [TestMethod]
        public void CinemaValidationTest()
        {
            Assert.IsTrue(goodCinema.Validate());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CinemaOpenAfterStart()
        {
            var badCinema = new Cinema()
            {
                OpenTime = new TimeSpan(1, 0, 0),
                CloseTime = new TimeSpan(0, 0, 0)
            };
            badCinema.Validate();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CinemaNullTheaters()
        {
            var cinema = new Cinema();

            cinema.Validate();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CinemaDuplicateTheaterNumber()
        {
            var cinema = new Cinema()
            {
                Theaters = new List<Theater>()
                {
                    new Theater() {TheaterNumber = 0 },
                    new Theater() {TheaterNumber = 1 },
                    new Theater() {TheaterNumber = 1 },
                    new Theater() {TheaterNumber = 2 },
                    new Theater() {TheaterNumber = 3 },
                }
            };

            cinema.Validate();

        }


        [TestMethod]
        public void GetCinemas()
        {
            using (var context = new CinemaContext())
            {
                Assert.IsNotNull(context.Cinemas.FirstOrDefault());
            }
        }

        [TestMethod]
        public void AddCinema()
        {
            var cinema = goodCinema;

            using (var context = new CinemaContext())
            {
                context.Cinemas.Add(cinema);

                context.SaveChanges();

                var dbCinema = context.Cinemas.FirstOrDefault(c => c.CinemaId == cinema.CinemaId);
                Assert.IsNotNull(dbCinema);

                Assert.IsTrue(cinema.Name == dbCinema.Name);
                Assert.IsTrue(cinema.Address == dbCinema.Address);
                Assert.IsTrue(cinema.CloseTime == dbCinema.CloseTime);
                Assert.IsTrue(cinema.OpenTime == dbCinema.OpenTime);
            }
        }

        [TestMethod]
        public void AddCinemaWithTheaters()
        {
            var cinema = goodCinemaWithTheaters;

            using (var context = new CinemaContext())
            {
                context.Cinemas.Add(cinema);
                context.SaveChanges();

                var dbObect = context.Cinemas.FirstOrDefault(c => c.CinemaId == cinema.CinemaId);

                Assert.IsNotNull(dbObect);
                Assert.AreEqual(cinema.CinemaId, dbObect.CinemaId);
                Assert.AreEqual(cinema.Name, dbObect.Name);
                Assert.AreEqual(cinema.Address, dbObect.Address);
                Assert.AreEqual(cinema.OpenTime, dbObect.OpenTime);
                Assert.AreEqual(cinema.CloseTime, dbObect.CloseTime);
                Assert.AreEqual(cinema.Theaters.Count, dbObect.Theaters.Count);

                Assert.IsTrue(cinema.Theaters.SequenceEqual(dbObect.Theaters));

            }
        }

        [TestMethod]
        public void DeleteCinema()
        {
            var cinema = goodCinema;

            using (var context = new CinemaContext())
            {
                context.Cinemas.Add(cinema);

                context.SaveChanges();

                Assert.IsNotNull(context.Cinemas.FirstOrDefault(c => c.CinemaId == cinema.CinemaId));

                context.Cinemas.Remove(cinema);

                context.SaveChanges();

                Assert.IsNull(context.Cinemas.FirstOrDefault(c => c.CinemaId == cinema.CinemaId));
            }
        }

        [TestMethod]
        public void DeleteCinemaWithTheaters()
        {
            var cinema = goodCinemaWithTheaters;

            using (var context = new CinemaContext())
            {
                context.Cinemas.Add(cinema);

                context.SaveChanges();

                Assert.IsNotNull(context.Cinemas.FirstOrDefault(c => c.CinemaId == cinema.CinemaId));

                context.Cinemas.Remove(cinema);
                context.SaveChanges();

                Assert.IsNull(context.Cinemas.FirstOrDefault(c => c.CinemaId == cinema.CinemaId));

                cinema.Theaters.ToList().ForEach(t => 
                    Assert.IsNull(context.Theaters.FirstOrDefault(db => t.TheaterId == db.TheaterId))
                );
            }
        }


    }

    [TestClass]
    public class CinemaManagerWebTests
    {
        private CinemasController Cinemas =  new CinemasController();
        public CinemaManagerWebTests()
        {
            AutomapperConfiguration.Configure();
        }

        [TestMethod]
        public void TestAutomapper()
        {
            AutomapperConfiguration.Test();
        }

        [TestMethod]
        public async Task GetCinemas()
        {
            IList<CinemaDTO> allCinemas = await Cinemas.GetCinemas();

            Assert.IsNotNull(allCinemas);
        }
        [TestMethod]
        public async Task GetCinema()
        {
            IList<CinemaDTO> allCinemas = await Cinemas.GetCinemas();

            if(allCinemas.Count == 0)
            {
                Assert.Inconclusive("No Cinemas found in database.");
            }
            else
            {
                CinemaDTO testCinema = allCinemas.First();

                var fromController = await Cinemas.GetCinema(testCinema.CinemaId) as OkNegotiatedContentResult<CinemaDTO>;

                Assert.IsNotNull(fromController);
                Assert.AreEqual(testCinema.CinemaId, fromController.Content.CinemaId);

            }

        }

        [TestMethod]
        public async Task GetCinemaNotFound()
        {
            var result = await Cinemas.GetCinema(-1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutCinema()
        {
            IList<CinemaDTO> allCinemas = await Cinemas.GetCinemas();

            if (allCinemas.Count ==0)
            {
                Assert.Inconclusive();
            }
            else
            {
                CinemaDTO testCinema = allCinemas.First();
                int cinemaId = testCinema.CinemaId;

                testCinema.Name = "put_test";
                testCinema.Address = "put_test_address";

                CinemaDTO cinemaResult = 
                    (await Cinemas.GetCinema(cinemaId) as OkNegotiatedContentResult<CinemaDTO>).Content;

                Assert.AreEqual(testCinema.Name, cinemaResult.Name);

            }
        }

      

        [TestMethod]
        public async Task PostCinema()
        {
            var c = new CinemaDTO()
            {
                Name = "test_post",
                Address = "test_address",
                OpenTime = new TimeSpan(1, 0, 0),
                CloseTime = new TimeSpan(5, 0, 0)
            };

            var result = (await Cinemas.PostCinema(c) as CreatedAtRouteNegotiatedContentResult<CinemaDTO>)?.Content;

            Assert.IsNotNull(result);
            Assert.AreEqual(c.Name, result.Name);
            Assert.AreEqual(c.Address, result.Address);
            Assert.AreEqual(c.OpenTime, result.OpenTime);
            Assert.AreEqual(c.CloseTime, result.CloseTime);
        }

        [TestMethod]
        public async Task SearchMovies()
        {
            MoviesProvider m = new MoviesProvider();

            var movies = await m.SearchMoviesByTitle("Frozen");

            Assert.IsNotNull(movies);
            Assert.AreNotEqual(movies.Count, 0);

            Assert.IsTrue(movies[0].Title.Contains("Frozen"));

        }

        [TestMethod]
        public async Task GetMovieByImdbId()
        {
            MoviesProvider m = new MoviesProvider();

            var movies = await m.SearchMoviesByTitle("Frozen");

            Assert.IsNotNull(movies);

            var y = await m.GetMovieByImdbId(movies[0].ImdbID);

            Assert.IsNotNull(y);
        }
    }
}
