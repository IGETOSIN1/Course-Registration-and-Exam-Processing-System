using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_Application_Higher_Institution;

namespace Web_Application_Higher_Institution.Controllers
{
    public class StaffController : Controller
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
        public List<SelectListItem> display_title()
        {
            List<SelectListItem> list = new List<SelectListItem>()
           {
               new SelectListItem {Text="Miss",Value="Miss" },
               new SelectListItem {Text="Mr",Value="Mr" },
               new SelectListItem {Text="Mrs",Value="Mrs" },
               new SelectListItem {Text="Dr",Value="Dr" },
               new SelectListItem {Text="Prof",Value="Prof" },
               new SelectListItem {Text="Chief",Value="Chief" },
                         };
            return list;
        }
        public List<SelectListItem> display_position()
        {
            List<SelectListItem> list = new List<SelectListItem>()
           {
               new SelectListItem {Text="Academic Staff",Value="Academic Staff" },
               new SelectListItem {Text="Non-Academic Staff",Value="Non-Academic Staff" },
               new SelectListItem {Text="External Supervisor",Value="External Supervisor" },
               new SelectListItem {Text="Visitor",Value="Visitor" },
                         };
            return list;
        }
        // GET: Staff
        public ActionResult Index()
        {
            verify();
            return View(db.table_staff.ToList());
        }

        // GET: Staff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_staff table_staff = db.table_staff.Find(id);
            if (table_staff == null)
            {
                return HttpNotFound();
            }
            return View(table_staff);
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            verify();
            ViewBag.titl = display_title();
            ViewBag.positio = display_position();
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,title,first_name,last_name,full_name,qualification,phone,email,position,code,date_employed,registered_by,date,dat")] table_staff model)
        {
            ViewBag.titl = display_title();
            ViewBag.positio = display_position();
            string full_name = model.first_name + " " + model.last_name;
            string code = model.first_name + model.last_name;
            string reg_by = "";
            if (string.IsNullOrWhiteSpace(model.title) || string.IsNullOrWhiteSpace(model.first_name) || string.IsNullOrWhiteSpace(model.last_name) || string.IsNullOrWhiteSpace(model.qualification) || string.IsNullOrWhiteSpace(model.phone) || string.IsNullOrWhiteSpace(model.position))
            {
                ViewBag.Result = "*All Fields Are Required ...";
            }
            else
            {
                db.Database.ExecuteSqlCommand("INSERT INTO table_staff(title,first_name,last_name,full_name,qualification,phone,email,position,code,date_employed,registered_by,date)VALUES('" + model.title + "','" + model.first_name + "','" + model.last_name + "','" + full_name + "','" + model.qualification + "','" + model.phone + "','" + model.email + "','" + model.position + "','" + code + "','" + model.date_employed + "','" + reg_by + "',now()) on duplicate key update first_name=values(first_name),last_name=values(last_name),full_name=values(full_name),qualification=values(qualification),phone=values(phone),email=values(email),position=values(position),date_employed=values(date_employed),registered_by=values(registered_by),date=values(date)");
                ViewBag.Result = full_name + " Had Been Successfully Added";
            }
            return View(model);
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            ViewBag.titl = display_title();
            ViewBag.positio = display_position();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_staff table_staff = db.table_staff.Find(id);
            if (table_staff == null)
            {
                return HttpNotFound();
            }
            return View(table_staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,title,first_name,last_name,full_name,qualification,phone,email,position,code,date_employed,registered_by,date,dat")] table_staff model)
        {
            ViewBag.titl = display_title();
            ViewBag.positio = display_position();
            string full_name = model.first_name + " " + model.last_name;
            string code = model.first_name + model.last_name;
            string reg_by = "";
            if (string.IsNullOrWhiteSpace(model.title) || string.IsNullOrWhiteSpace(model.first_name) || string.IsNullOrWhiteSpace(model.last_name) || string.IsNullOrWhiteSpace(model.qualification) || string.IsNullOrWhiteSpace(model.phone) || string.IsNullOrWhiteSpace(model.position))
            {
                ViewBag.Result = "*All Fields Are Required ...";
            }
            else
            {
                try
                {
                    db.Database.ExecuteSqlCommand("update table_staff set title='" + model.title + "',first_name='" + model.first_name + "',last_name='" + model.last_name + "',full_name='" + full_name + "',qualification='" + model.qualification + "',phone='" + model.phone + "',email='" + model.email + "',position='" + model.position + "',code='" + code + "',date_employed='" + model.date_employed + "' where p_id='" + model.p_id + "'");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            return View(model);
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_staff table_staff = db.table_staff.Find(id);
            if (table_staff == null)
            {
                return HttpNotFound();
            }
            return View(table_staff);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* table_staff table_staff = db.table_staff.Find(id);
             db.table_staff.Remove(table_staff);
             db.SaveChanges();
             return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_staff where p_id='" + id + "'");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Response.Write("<script type='text/javascript'>alert('Unable to Delete, You should Edit instead of Delete for Data Integrity and Enhancement ...');</script>");
            }
            return View();
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
