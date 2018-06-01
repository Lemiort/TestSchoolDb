using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSchoolDB.Models
{
    public class ClassroomTeacher
    {
        public int ClassroomTeacherId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public ICollection<Class> Classes { get; set; }
    }
}