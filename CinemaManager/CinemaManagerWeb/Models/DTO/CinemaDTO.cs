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

        /// <summary>
        /// Provides easier access to time for front-end modifications
        /// </summary>
        public int OpenTimeHours
        {
            get
            {
                return OpenTime.Hours;
            }
            set
            {
                if(value>=0 && value<24)
                {
                    OpenTime = new TimeSpan(value, OpenTime.Minutes, OpenTime.Seconds);
                }
            }
        }

        public int OpenTimeMinutes
        {
            get
            {
                return OpenTime.Minutes;
            }
            set
            {
                //Time unlikely to change. Magic numbers should be acceptable here...
                if (value >= 0 && value < 60)
                {
                    OpenTime = new TimeSpan(OpenTime.Hours, value, OpenTime.Seconds);
                }
            }
        }

        public TimeSpan CloseTime { get; set; }

        public int CloseTimeHours
        {
            get
            {
                return CloseTime.Hours;
            }
            set
            {
                if (value >= 0 && value < 24)
                {
                    CloseTime = new TimeSpan(value, CloseTime.Minutes, CloseTime.Seconds);
                }
            }
        }

        public int CloseTimeMinutes
        {
            get
            {
                return CloseTime.Minutes;
            }
            set
            {
                if (value >= 0 && value < 60)
                {
                    CloseTime = new TimeSpan(CloseTime.Hours, value, CloseTime.Seconds);
                }
            }
        }

        public List<TheaterDTO> Theaters { get; set; }
    }
}