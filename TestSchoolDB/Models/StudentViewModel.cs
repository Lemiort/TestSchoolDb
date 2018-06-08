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


        public static explicit operator StudentViewModel(Student student)
        {
            StudentViewModel studentViewModel = new StudentViewModel();
            studentViewModel.StudentId = student.StudentId;
            studentViewModel.FirstName = student.FirstName;
            studentViewModel.MiddleName = student.MiddleName;
            studentViewModel.LastName = student.LastName;
            if(student.StudentClass != null)
                studentViewModel.StudentClassId = student.StudentClass.ClassId;
            using (SchoolContext context = new SchoolContext())
            {
                studentViewModel.Classes = new List<SelectListItem>();
                foreach (var item in context.Classes)
                {
                    var c = new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ClassId.ToString()
                    };
                    if(item.ClassId == studentViewModel.StudentClassId)
                    {
                        c.Selected = true;
                    }

                    (studentViewModel.Classes as List<SelectListItem>).Add(c);
                }
            }
            return studentViewModel;
        }

        public static explicit operator Student(StudentViewModel studentViewModel)
        {
            using (SchoolContext context = new SchoolContext())
            {
               var student = context.Students
                    .FirstOrDefault(s=>s.StudentId == studentViewModel.StudentId);
                if (student == null)
                    student = new Student();
                student.FirstName = studentViewModel.FirstName;
                student.LastName = studentViewModel.LastName;
                student.MiddleName = studentViewModel.MiddleName;

                student.ClassId = studentViewModel.StudentClassId;
                //student.StudentClass = context.Classes.FirstOrDefault(c => c.ClassId == studentViewModel.StudentClassId);
                return student;
            }
        }
    }
}