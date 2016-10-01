using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    public static class Constants
    {
        public static readonly TimeSpan MAX_CLOSE_TIME = new TimeSpan(23, 59, 59);
        public static readonly TimeSpan MIN_OPEN_TIME = new TimeSpan(0, 0, 0);

    }
}
