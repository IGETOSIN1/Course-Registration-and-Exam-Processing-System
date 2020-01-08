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
    public class register_courseController : Controller
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
        public SelectList display_year()
        {
            SelectList sel = new SelectList(db.table_calendar_year.OrderByDescending(p => p.calendar_year).Select(p => p.calendar_year).ToList());
            return sel;
        }

        public SelectList display_level()
        {
            SelectList sel = new SelectList(db.table_level_add.OrderBy(p => p.Level).Select(p => p.Level).ToList());
            return sel;
        }

        public SelectList display_semester()
        {
            SelectList sel = new SelectList(db.table_semester_add.OrderBy(p => p.Semester).Select(p => p.Semester).ToList());
            return sel;
        }

        public SelectList display_course()
        {
            SelectList sel = new SelectList(db.table_course_add.OrderBy(p => p.course).Select(p => p.course).ToList());
            return sel;
        }

        // GET: register_course
        public ActionResult Index()
        {
            verify();
            string mat = (string)Session["matric"];
            return View(db.table_course_register.Where(p => p.matric_no == mat).OrderByDescending(p => p.p_id).ToList());
        }

        // GET: register_course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_course_register table_course_register = db.table_course_register.Find(id);
            if (table_course_register == null)
            {
                return HttpNotFound();
            }
            return View(table_course_register);
        }

        // GET: register_course/Create
        public ActionResult Create()
        {
            verify();
            ViewBag.calendar_yea = display_year();
            ViewBag.leve = display_level();
            ViewBag.semeste = display_semester();
            ViewBag.cours = display_course();
            return View();
        }

        // POST: register_course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,student_name,matric_no,course,course_code,code,unit,status,level,Semester,calendar_year,date,dat")] table_course_register model)
        {
            ViewBag.calendar_yea = display_year();
            ViewBag.leve = display_level();
            ViewBag.semeste = display_semester();
            ViewBag.cours = display_course();
            
            if (string.IsNullOrWhiteSpace(model.calendar_year) || string.IsNullOrWhiteSpace(model.level) || string.IsNullOrWhiteSpace(model.Semester) || string.IsNullOrWhiteSpace(model.course))
            {
                ViewBag.Result = "*All Fields Are Compulsory ...";
            }
            else
            {
                var co = db.table_course_add.Where(p => p.course == model.course).Select(p => p.course_code).FirstOrDefault();
                var ca = db.table_course_add.Where(p => p.course == model.course).Select(p => p.Unit).FirstOrDefault();
                string matric_no = (string)Session["matric"]; // "001";//session
                string student_name = (string)Session["name"]; //"Henry1 PILLAR1";// db.table_students.Where(p => p.matric_no == matric_no).Select(p => p.student_name).FirstOrDefault();
                string status = (string)Session["role"];
                string code = matric_no + model.course + co;
                db.Database.ExecuteSqlCommand("insert into table_course_register(student_name,matric_no,course,course_code,code,unit,status,level,semester,Calendar_year,date)values('" + student_name + "','" + matric_no + "','" + model.course + "','" + co + "','" + code + "','" + ca + "','" + status + "','" + model.level + "','" + model.Semester + "','" + model.calendar_year + "',now()) on duplicate key update code=values(code)");
                ViewBag.Result = "Registration of " + model.course + " For " + model.level + "(" + model.Semester + ") Was Successfull ...";
            }
            return View(model);
        }

        // GET: register_course/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_course_register table_course_register = db.table_course_register.Find(id);
            if (table_course_register == null)
            {
                return HttpNotFound();
            }
            return View(table_course_register);
        }

        // POST: register_course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,student_name,matric_no,course,course_code,code,unit,status,level,Semester,registered_by,date,dat")] table_course_register table_course_register)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(table_course_register).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            return View(table_course_register);
        }

        // GET: register_course/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_course_register table_course_register = db.table_course_register.Find(id);
            if (table_course_register == null)
            {
                return HttpNotFound();
            }
            return View(table_course_register);
        }

        // POST: register_course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /*table_course_register table_course_register = db.table_course_register.Find(id);
            db.table_course_register.Remove(table_course_register);
            db.SaveChanges();
            return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_course_register where p_id='" + id + "'");
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
