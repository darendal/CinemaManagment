using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaManagerWeb.Models.DTO
{
    public class MovieLeaseDTO
    {
        public int ID { get; set; }

        [Required]
        public int LengthOfEngagementInWeeks { get; set; }

        /// <summary>
        /// Per-week base allowance in cents
        /// </summary>
        public int HouseAllowance { get; set; }

        /// <summary>
        /// Percent of the gross profit kept by the theater
        /// </summary>
        public decimal GrossBoxOfficePercent { get; set; }

        public MovieDTO Movie { get; set; }

        public List<CinemaDTO> Cinemas { get; set; }
    }
}