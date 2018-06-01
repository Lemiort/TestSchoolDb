using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestSchoolDB.Models;

namespace TestSchoolDB.DAL
{
    public class SchoolContext:DbContext
    {
        public SchoolContext():base("SchoolContext")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<ClassroomTeacher> ClassroomTeachers { get; set; }
    }
}