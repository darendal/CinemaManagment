using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagerWeb.Models.DTO
{
    public class CinemaDTO
    {
        public int CinemaId { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public TimeSpan OpenTime { get; set; }

        public TimeSpan CloseTime { get; set; }

        public List<TheaterDTO> Theaters { get; set; }
    }
}