using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SealMarket.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirsrtName { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string City { get; set; }

        public User
        (
            int id,
            string firsrtName,
            string middlename,
            string lastname,
            DateOnly birthDate,
            string city
        )
        {
            Id = id;
            FirsrtName = firsrtName;
            Middlename = middlename;
            Lastname = lastname;
            BirthDate = birthDate;
            City = city;
        }
    }
}
