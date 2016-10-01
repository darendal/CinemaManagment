using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CinemaManager;
using CinemaManager.DataAccess;
using System.Linq;
using System.Collections.Generic;

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
}
