using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    [DataContract(IsReference = true)]
    public class Employee
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public Position Postition { get; set; }
        [DataMember]
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
