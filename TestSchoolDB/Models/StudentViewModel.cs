using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TestSchoolDB.DAL;

namespace TestSchoolDB.Models
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int StudentClassId { get; set; }
        public IEnumerable<SelectListItem> Classes { get; set; }

        public StudentViewModel()
        {

        }

        public StudentViewModel(Student student, SchoolContext context )
        {
            this.StudentId = student.StudentId;
            this.FirstName = student.FirstName;
            this.MiddleName = student.MiddleName;
            this.LastName = student.LastName;
            if(student.StudentClass != null)
                this.StudentClassId = student.StudentClass.ClassId;

            this.Classes = new List<SelectListItem>();
            foreach (var item in context.Classes)
            {
                var c = new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.ClassId.ToString()
                };
                if(item.ClassId == this.StudentClassId)
                {
                    c.Selected = true;
                }

                (this.Classes as List<SelectListItem>).Add(c);
            }
        }

        public Student ToStudent(SchoolContext context)
        {

            var student = context.Students
                .FirstOrDefault(s=>s.StudentId == this.StudentId);
            if (student == null)
                student = new Student();
            student.FirstName = this.FirstName;
            student.LastName = this.LastName;
            student.MiddleName = this.MiddleName;

            student.ClassId = this.StudentClassId;
            //student.StudentClass = context.Classes.FirstOrDefault(c => c.ClassId == studentViewModel.StudentClassId);
            return student;
            
        }
    }
}