using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    [DataContract(IsReference = true)]
    public class MovieLease
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CinemaId { get; set; }
        [DataMember]
        public int MovieId { get; set; }
        [DataMember]
        public int LengthOfEngagementInWeeks { get; set; }

        /// <summary>
        /// Per-week base allowance in cents
        /// </summary>
        [DataMember]
        public int HouseAllowance { get; set; }

        /// <summary>
        /// Percent of the gross profit kept by the theater
        /// </summary>
        [DataMember]
        public decimal GrossBoxOfficePercent { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Cinema Cinema { get; set; }

    }
}
