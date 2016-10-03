using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Position Postition { get; set; }

        public int CinemaId { get; set; }

        public virtual Cinema Cinema {get;set;}
    }

    public enum Position
    {
        Trainee = 1,
        Entry = 2,
        Manager = 3,
        Director = 4,
        CEO = 5
    }

}
