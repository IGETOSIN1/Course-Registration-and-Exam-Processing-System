using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Application_Higher_Institution.Controllers
{
    public class HomeController : Controller
    {
        private void verify()
        {
            try
            {
                if (Session["user"] == null)
                {
                    Response.Redirect("/Login.aspx");
                }
            }
            catch
            {
                Response.Redirect("/Login.aspx");
            }
        }
        public ActionResult Index()
        {
            verify();
            return View();
        }

        [HttpGet]
        public ActionResult Index_Student()
        {
            verify();
            return View();
        }

        [HttpPost]
        public ActionResult Index_Student(FormCollection fmc)
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Logout()
        {
            return Redirect("~/Login.aspx");
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}