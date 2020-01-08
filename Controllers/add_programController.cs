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
    public class add_programController : Controller
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
        public SelectList display_faculty()
        {
            SelectList sel = new SelectList(db.table_faculty_add.Select(p => p.faculty).Distinct().ToList());
            return sel;
        }
        public SelectList display_department()
        {
            SelectList sel = new SelectList(db.table_department_add.Select(p => p.department).Distinct().ToList());
            return sel;
        }

        // GET: add_program
        public ActionResult Index()
        {
            verify();
            return View(db.table_program_add.ToList());
        }

        // GET: add_program/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_program_add table_program_add = db.table_program_add.Find(id);
            if (table_program_add == null)
            {
                return HttpNotFound();
            }
            return View(table_program_add);
        }

        // GET: add_program/Create
        public ActionResult Create()
        {
            verify();
            ViewBag.facult = display_faculty();
            ViewBag.departmen = display_department();
            return View();
        }

        // POST: add_program/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,faculty,department,program,code,Registered_by,date,dat")] table_program_add model)
        {
            ViewBag.facult = display_faculty();
            ViewBag.departmen = display_department();
            if (string.IsNullOrWhiteSpace(model.faculty) || string.IsNullOrWhiteSpace(model.department) || string.IsNullOrWhiteSpace(model.program))
            {
                ViewBag.Result = "*All Fields Are Required ...";
            }
            else
            {
                string code = model.faculty + model.department + model.program;
                db.Database.ExecuteSqlCommand("insert into table_program_add(Faculty,Department,Program,Code,Date)values('" + model.faculty + "','" + model.department + "','" + model.program + "','" + code + "',now()) on duplicate key update program=values(program)");
                ViewBag.Result = model.program + " Had Been Successfully Added to Program/ Degree ...";
            }
            return View(model);
        }

        // GET: add_program/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            ViewBag.facult = display_faculty();
            ViewBag.departmen = display_department();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_program_add table_program_add = db.table_program_add.Find(id);
            if (table_program_add == null)
            {
                return HttpNotFound();
            }
            return View(table_program_add);
        }

        // POST: add_program/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,faculty,department,program,code,Registered_by,date,dat")] table_program_add model)
        {
            ViewBag.facult = display_faculty();
            ViewBag.departmen = display_department();
            /* if (ModelState.IsValid)
             {
                 db.Entry(model).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }*/
            if (string.IsNullOrWhiteSpace(model.faculty) || string.IsNullOrWhiteSpace(model.department) || string.IsNullOrWhiteSpace(model.program))
            {
                ViewBag.Result = "*All Fields Are Required ...";
            }
            else
            {
                try
                {
                    string code = model.faculty + model.department + model.program;
                    db.Database.ExecuteSqlCommand("update table_program_add set faculty='" + model.faculty + "',department='" + model.department + "',program='" + model.program + "' where p_id='" + model.p_id + "'");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            return View(model);
        }

        // GET: add_program/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_program_add table_program_add = db.table_program_add.Find(id);
            if (table_program_add == null)
            {
                return HttpNotFound();
            }
            return View(table_program_add);
        }

        // POST: add_program/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* table_program_add table_program_add = db.table_program_add.Find(id);
             db.table_program_add.Remove(table_program_add);
             db.SaveChanges();
             return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_program_add where p_id='" + id + "'");
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
