using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Application_Higher_Institution.Controllers
{
    public class ExtraController : Controller
    {
        private Entities_Institute db = new Entities_Institute();
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
        // GET: Extra/Create
        public ActionResult Change_Password()
        {
            verify();
            return View();
        }


        // POST: Extra/Create
        [HttpPost]
        public ActionResult Change_Password(FormCollection fmc)
        {
            verify();
            try
            {
                if (string.IsNullOrWhiteSpace(fmc["password1"]))
                {
                    ViewBag.Result = "Enter New Password ...";
                }
                else if (string.IsNullOrWhiteSpace(fmc["password2"]))
                {
                    ViewBag.Result = "Confirm New Password ...";
                }
                else if (fmc["password1"] != fmc["password2"])
                {
                    ViewBag.Result = "Password Does Not Match ...";
                }
                else
                {
                    string usr = (string)Session["user"];
                    db.Database.ExecuteSqlCommand("UPDATE table_user set password='" + fmc["password1"] + "' where user_name='" + usr + "'");
                    ViewBag.Result = "Password Successfully Saved ...";
                    // return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Extra/Create
        public ActionResult Configure_School()
        {
            verify();
            return View();
        }


        // POST: Extra/Create
        [HttpPost]
        public ActionResult Configure_School(table_school_details model)
        {
            verify();
            try
            {
                db.Database.ExecuteSqlCommand("Delete from table_school_details");
                model.Date = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
                db.table_school_details.Add(model);
                db.SaveChanges();
                ViewBag.Result = "Record Successfully Save ...";
                return View();
                // return RedirectToAction("Configure_School");
            }
            catch
            {
                return View();
            }
        }


    }
}
