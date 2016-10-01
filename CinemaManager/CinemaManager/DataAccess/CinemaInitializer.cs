using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager.DataAccess
{
    public class CinemaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CinemaContext>
    {
    }
}
