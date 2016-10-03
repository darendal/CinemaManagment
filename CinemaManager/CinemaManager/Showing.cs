using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    public class Showing
    {
        public int Id { get; set; }

        public int MovieId {get;set;}

        public int TheaterId { get; set; }

        public TimeSpan StartTime { get; set; }

        //public virtual Movie Movie {get;set;}
        public virtual Theater Theater {get;set;}
    }
}
