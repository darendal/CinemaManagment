using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CinemaManager;

namespace CinemaManagerTests
{
    [TestClass]
    public class CinemaManagerTests
    {
        Cinema goodCinema = new Cinema();

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

    }
}
