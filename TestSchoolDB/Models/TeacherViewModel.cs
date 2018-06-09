using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSchoolDB.DAL;

namespace TestSchoolDB.Models
{
    public class TeacherViewModel
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<SelectListItem> Classes { get; set; }
        public IEnumerable<int> SelectedClasses { get; set; }

        public TeacherViewModel()
        {

        }

        public TeacherViewModel(Teacher teacher, SchoolContext context)
        {
            this.TeacherId = teacher.TeacherId;
            this.FirstName = teacher.FirstName;
            this.MiddleName = teacher.MiddleName;
            this.LastName = teacher.LastName;


            this.Classes = new List<SelectListItem>();
            foreach (var item in context.Classes)
            {
                if (item.TeacherId == null || item.TeacherId == teacher.TeacherId)
                {
                    var c = new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ClassId.ToString()
                    };
                    if (item.TeacherId == teacher.TeacherId)
                    {
                        c.Selected = true;
                    }

                    (this.Classes as List<SelectListItem>).Add(c);
                }
            }

        }

        public Teacher ToTeacher(SchoolContext context)
        {
            var teacher = context.Teachers
                .Include(t=>t.Classes)
                    .FirstOrDefault(t => t.TeacherId == this.TeacherId);
            if (teacher == null)
                teacher = new Teacher() { Classes = new List<Class>() };
            teacher.FirstName = this.FirstName;
            teacher.LastName = this.LastName;
            teacher.MiddleName = this.MiddleName;
            teacher.Classes.Clear();

            if (this.SelectedClasses != null)
            {
                foreach (var item in this.SelectedClasses)
                {
                    var selectedClass = context.Classes
                        //.AsNoTracking()
                        //.Include(c=>c.Teacher)
                        .FirstOrDefault(c => c.ClassId == item);
                    //selectedClass.TeacherId = teacher.TeacherId;
                    //selectedClass.Teacher = teacher;
                    teacher.Classes.Add(selectedClass);

                    //context.Entry(selectedClass).State = EntityState.Modified;
                }
            }
            return teacher;
        }
    }
}