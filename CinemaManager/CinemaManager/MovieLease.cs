using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    public class MovieLease
    {

        public int ID { get; set; }

        public int CinemaId { get; set; }

        public int MovieId { get; set; }

        public int LengthOfEngagementInWeeks { get; set; }

        /// <summary>
        /// Per-week base allowance in cents
        /// </summary>

        public int HouseAllowance { get; set; }

        /// <summary>
        /// Percent of the gross profit kept by the theater
        /// </summary>

        public decimal GrossBoxOfficePercent { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Cinema Cinema { get; set; }

    }
}
