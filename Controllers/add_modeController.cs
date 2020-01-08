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
    public class add_modeController : Controller
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
        // GET: add_mode
        public ActionResult Index()
        {
            verify();
            return View(db.table_mode_add.ToList());
        }

        // GET: add_mode/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_mode_add table_mode_add = db.table_mode_add.Find(id);
            if (table_mode_add == null)
            {
                return HttpNotFound();
            }
            return View(table_mode_add);
        }

        // GET: add_mode/Create
        public ActionResult Create()
        {
            verify();
            return View();
        }

        // POST: add_mode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,mode,dat")] table_mode_add model)
        {
            if (string.IsNullOrWhiteSpace(model.mode))
            {
                ViewBag.Result = "*Enter Mode of Study to Add ...";
            }
            else
            {
                db.Database.ExecuteSqlCommand("insert into table_mode_add(mode)values('" + model.mode + "') on duplicate key update mode=values(mode)");
                ViewBag.Result = model.mode + " Had Been Successfully Added ...";
            }

            return View(model);
        }

        // GET: add_mode/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_mode_add table_mode_add = db.table_mode_add.Find(id);
            if (table_mode_add == null)
            {
                return HttpNotFound();
            }
            return View(table_mode_add);
        }

        // POST: add_mode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,mode,dat")] table_mode_add table_mode_add)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(table_mode_add).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            return View(table_mode_add);
        }

        // GET: add_mode/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_mode_add table_mode_add = db.table_mode_add.Find(id);
            if (table_mode_add == null)
            {
                return HttpNotFound();
            }
            return View(table_mode_add);
        }

        // POST: add_mode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* table_mode_add table_mode_add = db.table_mode_add.Find(id);
             db.table_mode_add.Remove(table_mode_add);
             db.SaveChanges();
             return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_mode_add where p_id='" + id + "'");
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
