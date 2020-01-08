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
    public class add_semesterController : Controller
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
        // GET: add_semester
        public ActionResult Index()
        {
            verify();
            return View(db.table_semester_add.ToList());
        }

        // GET: add_semester/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_semester_add table_semester_add = db.table_semester_add.Find(id);
            if (table_semester_add == null)
            {
                return HttpNotFound();
            }
            return View(table_semester_add);
        }

        // GET: add_semester/Create
        public ActionResult Create()
        {
            verify();
            return View();
        }

        // POST: add_semester/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,Semester,dat")] table_semester_add model)
        {
            /* if (ModelState.IsValid)
             {
                 db.table_semester_add.Add(table_semester_add);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }
             return View(table_semester_add);*/

            if (string.IsNullOrWhiteSpace(model.Semester))
            {
                ViewBag.Result = "Enter Semester To Add ...";
            }
            else
            {
                db.Database.ExecuteSqlCommand("INSERT INTO table_semester_add(Semester)values('" + model.Semester + "') ON DUPLICATE KEY UPDATE semester=values(semester)");
                ViewBag.Result = model.Semester + " Had Been Successfully Added ...";
            }
            return View(model);

        }

        // GET: add_semester/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_semester_add table_semester_add = db.table_semester_add.Find(id);
            if (table_semester_add == null)
            {
                return HttpNotFound();
            }
            return View(table_semester_add);
        }

        // POST: add_semester/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,Semester,dat")] table_semester_add table_semester_add)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(table_semester_add).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            return View(table_semester_add);
        }

        // GET: add_semester/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_semester_add table_semester_add = db.table_semester_add.Find(id);
            if (table_semester_add == null)
            {
                return HttpNotFound();
            }
            return View(table_semester_add);
        }

        // POST: add_semester/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* table_semester_add table_semester_add = db.table_semester_add.Find(id);
             db.table_semester_add.Remove(table_semester_add);
             db.SaveChanges();
             return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_semester_add where p_id='" + id + "'");
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
