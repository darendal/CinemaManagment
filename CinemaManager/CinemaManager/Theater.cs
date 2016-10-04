using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CinemaManager
{

    public class Theater
    {
        /// <summary>
        /// Unique Theater Identifier
        /// </summary>

        public int TheaterId { get; set; }

        [Index("CinemaIdAndTheaterNumber",IsUnique =true,Order =0)]

        public int CinemaId { get; set; }

        /// <summary>
        /// Theater number in the relevant cinema
        /// </summary>
        [Index("CinemaIdAndTheaterNumber",IsUnique =true,Order =1)]

        public int TheaterNumber { get; set; }

        /// <summary>
        /// Maximum # of seats in the theater
        /// </summary>

        public int MaximumCapacity { get; set; }

        
        public virtual Cinema Cinema { get; set; }
    }
}