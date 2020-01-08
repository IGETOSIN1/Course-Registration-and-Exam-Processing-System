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
    public class add_statusController : Controller
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
        // GET: add_status
        public ActionResult Index()
        {
            verify();
            return View(db.table_status_add.ToList());
        }

        // GET: add_status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_status_add table_status_add = db.table_status_add.Find(id);
            if (table_status_add == null)
            {
                return HttpNotFound();
            }
            return View(table_status_add);
        }

        // GET: add_status/Create
        public ActionResult Create()
        {
            verify();
            return View();
        }

        // POST: add_status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,status,dat")] table_status_add model)
        {
            /* if (ModelState.IsValid)
             {
                 db.table_status_add.Add(table_status_add);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }*/
            if (string.IsNullOrWhiteSpace(model.status))
            {
                ViewBag.Result = "*Enter Status to Add ...";
            }
            else
            {
                db.Database.ExecuteSqlCommand("insert into table_status_add(status)values('" + model.status + "') on duplicate key update status=values(status)");
                ViewBag.Result = model.status + " Had Been Successfully Added ...";
            }
            return View(model);
        }

        // GET: add_status/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_status_add table_status_add = db.table_status_add.Find(id);
            if (table_status_add == null)
            {
                return HttpNotFound();
            }
            return View(table_status_add);
        }

        // POST: add_status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,status,dat")] table_status_add table_status_add)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(table_status_add).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            return View(table_status_add);
        }

        // GET: add_status/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_status_add table_status_add = db.table_status_add.Find(id);
            if (table_status_add == null)
            {
                return HttpNotFound();
            }
            return View(table_status_add);
        }

        // POST: add_status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* table_status_add table_status_add = db.table_status_add.Find(id);
             db.table_status_add.Remove(table_status_add);
             db.SaveChanges();
             return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_status_add where p_id='" + id + "'");
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
