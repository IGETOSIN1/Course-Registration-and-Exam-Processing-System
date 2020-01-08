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
    public class add_courseController : Controller
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
        public SelectList display_level()
        {
            SelectList sel = new SelectList(db.table_level_add.OrderBy(p => p.Level).Select(p => p.Level).ToList());
            return sel;
        }
        // GET: add_course
        public ActionResult Index()
        {
            verify();
            return View(db.table_course_add.ToList());
        }

        // GET: add_course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_course_add table_course_add = db.table_course_add.Find(id);
            if (table_course_add == null)
            {
                return HttpNotFound();
            }
            return View(table_course_add);
        }

        // GET: add_course/Create
        public ActionResult Create()
        {
            verify();
            ViewBag.leve = display_level();
            return View();
        }

        // POST: add_course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,course,course_code,registered_by,Unit,date,dat,Level")] table_course_add model)
        {
            if (string.IsNullOrWhiteSpace(model.course) || string.IsNullOrWhiteSpace(model.course_code) || string.IsNullOrWhiteSpace(model.Unit) || string.IsNullOrWhiteSpace(model.Level))
            {
                ViewBag.Result = "*All Fields are Required for Confirmation ...";
            }
            else
            {
                db.Database.ExecuteSqlCommand("insert into table_course_add(course,course_code,Unit,Level,date)values('" + model.course + "','" + model.course_code + "','"+model.Unit+ "','"+model.Level+ "',now()) ON DUPLICATE KEY UPDATE course=values(course),course_code=values(course_code),Unit=values(Unit),Level=values(Level)");
                ViewBag.Result = model.course + " Had Been Successfully Confirmed/ Added ...";
                model.course = null;
                model.course_code = "";
                /*  db.table_course_add.Add(model);
                  db.SaveChanges();*/
                // return RedirectToAction("Index");*/
            }

            ViewBag.leve = display_level();
            return View(model);
        }

        // GET: add_course/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            ViewBag.leve = display_level();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_course_add table_course_add = db.table_course_add.Find(id);
            if (table_course_add == null)
            {
                return HttpNotFound();
            }
            return View(table_course_add);
        }

        // POST: add_course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,course,course_code,registered_by,Unit,date,dat,Level")] table_course_add model)
        {
            /* if (ModelState.IsValid)
             {
                 db.Entry(table_course_add).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }*/
            if (string.IsNullOrWhiteSpace(model.course) || string.IsNullOrWhiteSpace(model.course_code) || string.IsNullOrWhiteSpace(model.Unit))
            {
                ViewBag.Result = "*All Fields are Required ...";
            }
            else
            {
                try
                {
                    // db.Database.ExecuteSqlCommand("insert into table_course_add(course,course_code,date)values('" + model.course + "','" + model.course_code + "',now()) ON DUPLICATE KEY UPDATE course=values(course),course_code=values(course_code)");
                    db.Database.ExecuteSqlCommand("update table_course_add set course='" + model.course + "',course_code='" + model.course_code + "',Unit='" + model.Unit + "',Level='" + model.Level + "' where p_id='" + model.p_id + "'");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            ViewBag.leve = display_level();
            return View(model);

        }

        // GET: add_course/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_course_add table_course_add = db.table_course_add.Find(id);
            if (table_course_add == null)
            {
                return HttpNotFound();
            }
            return View(table_course_add);
        }

        // POST: add_course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* table_course_add table_course_add = db.table_course_add.Find(id);
             db.table_course_add.Remove(table_course_add);
             db.SaveChanges();
             return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_course_add where p_id='" + id + "'");
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
