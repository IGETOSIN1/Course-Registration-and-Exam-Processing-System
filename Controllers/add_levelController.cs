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
    public class add_levelController : Controller
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
        // GET: add_level
        public ActionResult Index()
        {
            verify();
            return View(db.table_level_add.ToList());
        }

        // GET: add_level/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_level_add table_level_add = db.table_level_add.Find(id);
            if (table_level_add == null)
            {
                return HttpNotFound();
            }
            return View(table_level_add);
        }

        // GET: add_level/Create
        public ActionResult Create()
        {
            verify();
            return View();
        }

        // POST: add_level/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,Level,dat")] table_level_add model)
        {
            /*if (ModelState.IsValid)
            {
                db.table_level_add.Add(table_level_add);
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/
            if (string.IsNullOrWhiteSpace(model.Level))
            {
                ViewBag.Result = "Enter Level/ Year To Add ...";
            }
            else
            {
                db.Database.ExecuteSqlCommand("INSERT INTO table_level_add(Level)values('" + model.Level + "') ON DUPLICATE KEY UPDATE level=values(level)");
                ViewBag.Result = model.Level + " Had Been Successfully Added ...";
            }
            return View(model);
        }

        // GET: add_level/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_level_add table_level_add = db.table_level_add.Find(id);
            if (table_level_add == null)
            {
                return HttpNotFound();
            }
            return View(table_level_add);
        }

        // POST: add_level/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,Level,dat")] table_level_add table_level_add)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(table_level_add).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            return View(table_level_add);
        }

        // GET: add_level/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_level_add table_level_add = db.table_level_add.Find(id);
            if (table_level_add == null)
            {
                return HttpNotFound();
            }
            return View(table_level_add);
        }

        // POST: add_level/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* table_level_add table_level_add = db.table_level_add.Find(id);
             db.table_level_add.Remove(table_level_add);
             db.SaveChanges();
             return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_level_add where p_id='" + id + "'");
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
