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
    public class add_year : Controller
    {
        private Entities_Institute db = new Entities_Institute();

        // GET: add_year
        public ActionResult Index()
        {
            return View(db.table_calendar_year.ToList());
        }

        // GET: add_year/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_calendar_year table_calendar_year = db.table_calendar_year.Find(id);
            if (table_calendar_year == null)
            {
                return HttpNotFound();
            }
            return View(table_calendar_year);
        }

        // GET: add_year/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: add_year/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,calendar_year,dat")] table_calendar_year table_calendar_year)
        {
            if (ModelState.IsValid)
            {
                db.table_calendar_year.Add(table_calendar_year);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table_calendar_year);
        }

        // GET: add_year/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_calendar_year table_calendar_year = db.table_calendar_year.Find(id);
            if (table_calendar_year == null)
            {
                return HttpNotFound();
            }
            return View(table_calendar_year);
        }

        // POST: add_year/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,calendar_year,dat")] table_calendar_year table_calendar_year)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table_calendar_year).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table_calendar_year);
        }

        // GET: add_year/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_calendar_year table_calendar_year = db.table_calendar_year.Find(id);
            if (table_calendar_year == null)
            {
                return HttpNotFound();
            }
            return View(table_calendar_year);
        }

        // POST: add_year/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            table_calendar_year table_calendar_year = db.table_calendar_year.Find(id);
            db.table_calendar_year.Remove(table_calendar_year);
            db.SaveChanges();
            return RedirectToAction("Index");
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
