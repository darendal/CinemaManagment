using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagerWeb.Models.DTO
{
    public class TheaterDTO
    {
        public int CinemaId { get; set; }
        public int TheaterId { get; set; }
        public int TheaterNumber { get; set; }
        public int MaximumCapacity { get; set; }

    }
}