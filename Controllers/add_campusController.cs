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
    public class add_campusController : Controller
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

        // GET: add_campus
        public ActionResult Index()
        {
            verify();
            return View(db.table_campus_add.ToList());
        }

        // GET: add_campus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_campus_add table_campus_add = db.table_campus_add.Find(id);
            if (table_campus_add == null)
            {
                return HttpNotFound();
            }
            return View(table_campus_add);
        }

        // GET: add_campus/Create
        public ActionResult Create()
        {
            verify();
            return View();
        }

        // POST: add_campus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,campus,dat")] table_campus_add model)
        {
            if (string.IsNullOrWhiteSpace(model.campus))
            {
                ViewBag.Result = "*Enter Campus to Add ...";
            }
            else
            {
                db.Database.ExecuteSqlCommand("insert into table_campus_add(campus)values('" + model.campus + "') on duplicate key update campus=values(campus)");
                ViewBag.Result = model.campus + " Had Been Successfully Added ...";
            }

            return View(model);
        }

        // GET: add_campus/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_campus_add table_campus_add = db.table_campus_add.Find(id);
            if (table_campus_add == null)
            {
                return HttpNotFound();
            }
            return View(table_campus_add);
        }

        // POST: add_campus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,campus,dat")] table_campus_add table_campus_add)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(table_campus_add).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            return View(table_campus_add);
        }

        // GET: add_campus/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_campus_add table_campus_add = db.table_campus_add.Find(id);
            if (table_campus_add == null)
            {
                return HttpNotFound();
            }
            return View(table_campus_add);
        }

        // POST: add_campus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* table_campus_add table_campus_add = db.table_campus_add.Find(id);
             db.table_campus_add.Remove(table_campus_add);
             db.SaveChanges();
             return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_campus_add where p_id='" + id + "'");
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
