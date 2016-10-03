using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public Rating Rating { get; set; }

    }

    public enum Rating
    {
        NR = 0,
        G = 1,
        PG = 2,
        PG13 = 3,
        R = 4,
        A = 5
    }
}
