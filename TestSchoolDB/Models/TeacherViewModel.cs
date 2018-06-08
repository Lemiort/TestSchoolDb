using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestSchoolDB.Models
{
    public class TeacherViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public MultiSelectList Classes { get; set; }

        public static explicit operator TeacherViewModel(Teacher teacher)
        {
            StudentViewModel teacherViewModel = new StudentViewModel();
            teacherViewModel.StudentId = teacher.StudentId;
            teacherViewModel.FirstName = teacher.FirstName;
            teacherViewModel.MiddleName = teacher.MiddleName;
            teacherViewModel.LastName = teacher.LastName;
            if (teacher.StudentClass != null)
                teacherViewModel.StudentClassId = teacher.StudentClass.ClassId;
            using (SchoolContext context = new SchoolContext())
            {
                teacherViewModel.Classes = new List<SelectListItem>();
                foreach (var item in context.Classes)
                {
                    var c = new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ClassId.ToString()
                    };
                    if (item.ClassId == teacherViewModel.StudentClassId)
                    {
                        c.Selected = true;
                    }

                    (teacherViewModel.Classes as List<SelectListItem>).Add(c);
                }
            }
            return teacherViewModel;
        }

        public static explicit operator Student(StudentViewModel studentViewModel)
        {
            using (SchoolContext context = new SchoolContext())
            {
                var student = context.Students
                     .FirstOrDefault(s => s.StudentId == studentViewModel.StudentId);
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