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
    public class add_facultyController : Controller
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
        // GET: add_faculty
        public ActionResult Index()
        {
            verify();
            return View(db.table_faculty_add.ToList());
        }

        // GET: add_faculty/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_faculty_add table_faculty_add = db.table_faculty_add.Find(id);
            if (table_faculty_add == null)
            {
                return HttpNotFound();
            }
            return View(table_faculty_add);
        }

        // GET: add_faculty/Create
        public ActionResult Create()
        {
            verify();
            return View();
        }

        // POST: add_faculty/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,faculty,registered_by,date,dat")] table_faculty_add model)
        {
            /* if (ModelState.IsValid)
             {
                 db.table_faculty_add.Add(table_faculty_add);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }*/
            if (string.IsNullOrWhiteSpace(model.faculty))
            {
                ViewBag.Result = "*Enter the Name of Faculty to Add ...";
            }
            else
            {
                db.Database.ExecuteSqlCommand("INSERT INTO Table_faculty_add(faculty,date)values('" + model.faculty + "',now()) on duplicate key update faculty=values(faculty)");
                ViewBag.Result = model.faculty+" Had Been Successfully Added!";
            }
            return View(model);
        }

        // GET: add_faculty/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_faculty_add table_faculty_add = db.table_faculty_add.Find(id);
            if (table_faculty_add == null)
            {
                return HttpNotFound();
            }
            return View(table_faculty_add);
        }

        // POST: add_faculty/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,faculty,registered_by,date,dat")] table_faculty_add model)
        {
            if (string.IsNullOrWhiteSpace(model.faculty))
            {
                ViewBag.Result = "*All Fields Are Required ...";
            }
            else
            {
                try
                {
                    //  db.Database.ExecuteSqlCommand("INSERT INTO Table_faculty_add(faculty,date)values('" + model.faculty + "',now()) on duplicate key update faculty=values(faculty)");
                    db.Database.ExecuteSqlCommand("UPDATE table_faculty_add set faculty='" + model.faculty + "' where p_id='" + model.p_id + "'");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            return View(model);

        }

        // GET: add_faculty/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_faculty_add table_faculty_add = db.table_faculty_add.Find(id);
            if (table_faculty_add == null)
            {
                return HttpNotFound();
            }
            return View(table_faculty_add);
        }

        // POST: add_faculty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* table_faculty_add table_faculty_add = db.table_faculty_add.Find(id);
             db.table_faculty_add.Remove(table_faculty_add);
             db.SaveChanges();
             return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_faculty_add where p_id='" + id + "'");
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
