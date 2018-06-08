using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSchoolDB.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}