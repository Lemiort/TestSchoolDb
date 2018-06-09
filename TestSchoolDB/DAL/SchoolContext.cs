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
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // configures one-to-many relationship
        //    modelBuilder.Entity<Class>()
        //        .HasOptional<Teacher>(c => c.Teacher)
        //        .WithMany(t => t.Classes)
        //        .HasForeignKey<int?>(c => c.TeacherId);
        //}

    }
}
