using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSchoolDB.DAL;

namespace TestSchoolDB.Controllers
{
    public class HomeController : Controller
    {
        SchoolContext schoolContext;

        public HomeController()
        {
            schoolContext = new SchoolContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}