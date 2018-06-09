using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestSchoolDB.DAL;
using TestSchoolDB.Models;

namespace TestSchoolDB.Controllers
{
    public class TeachersController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Teachers
        public ActionResult Index()
        {
            var teachers = db.Teachers
                .Include(t => t.Classes)
                .ToList();
            return View(teachers);
        }

        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            var teacher = new Teacher();
            return View( new TeacherViewModel(teacher, db));
        }

        // POST: Teachers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "TeacherId,FirstName,MiddleName,LastName")]*/ TeacherViewModel teacherViewModel)
        {
            if (ModelState.IsValid)
            {
                var teacher = teacherViewModel.ToTeacher(db);
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacherViewModel);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers
                .AsNoTracking()
                .FirstOrDefault(t =>t.TeacherId == id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            var teacherViewModel = new TeacherViewModel(teacher, db);
            return View(teacherViewModel);
        }

        // POST: Teachers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "TeacherId,FirstName,MiddleName,LastName")]*/ TeacherViewModel teacherViewModel)
        {
            if (ModelState.IsValid)
            {
                var teacher = teacherViewModel.ToTeacher(db);
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacherViewModel);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
