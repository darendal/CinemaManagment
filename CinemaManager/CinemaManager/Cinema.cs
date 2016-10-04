using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CinemaManager
{
    public class Cinema
    {
        public Cinema()
        {
            CinemaId = -1;
            OpenTime = new TimeSpan(10, 00, 00);
            CloseTime = new TimeSpan(22, 00, 00);
        }

        public int CinemaId { get;  set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Theater> Theaters { get; set; }

        public TimeSpan OpenTime { get; set;}

        public TimeSpan CloseTime { get; set; }
        public virtual List<MovieLease> MovieLeases { get; set; }

        public virtual List<Employee> Employees { get; set; }


        public bool Validate()
        {
            Validation.isGreaterThanOrEqual(OpenTime, Constants.MIN_OPEN_TIME);
            Validation.isLessThanOrEqual(CloseTime, Constants.MAX_CLOSE_TIME);
            Validation.isGreaterThan(CloseTime, OpenTime, "Open time cannot be after close time");
           if(Theaters != null)
            {
                var h = new HashSet<int>();
                if(Theaters.Any( item => !h.Add(item.TheaterNumber)))
                {
                    throw new ArgumentException("Theaters in a cinema cannot have duplicate Theater Numbers");
                }
            }


            return true;//Validation throws errors, doesn't return false;
        }


    }
}
