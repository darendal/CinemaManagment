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

        private int CinemaId { get;  set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Theater> Theaters { get; private set; }

        public TimeSpan OpenTime { get; set;}

        public TimeSpan CloseTime { get; set; }

        public bool Validate()
        {
            Validation.isGreaterThan(CloseTime, OpenTime, "Open time cannot be after close time");
            Validation.isNotNull(Theaters, Utility.GetPropertyName(()=>this.Theaters));

            return true;//Validation throws errors, doesn't return false;
        }

    }
}
