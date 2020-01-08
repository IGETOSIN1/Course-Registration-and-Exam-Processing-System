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
    public class ResultController : Controller
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
        // GET: Result
        public ActionResult Index()
        {
            verify();
            return View(db.table_result.ToList());
        }
        public SelectList display_academic_year()
        {
            SelectList sel = new SelectList(db.table_calendar_year.OrderBy(p=>p.calendar_year).Select(p => p.calendar_year).ToList());
            return sel;
        }
        public SelectList display_semester()
        {
            SelectList sel = new SelectList(db.table_semester_add.OrderBy(p=>p.Semester).Select(p => p.Semester).ToList());
            return sel;
        }
        public SelectList display_course()
        {
            SelectList sel = new SelectList(db.table_course_add.OrderBy(p=>p.course).Select(p => p.course).ToList());
            return sel;
        }
        public SelectList display_student()
        {
            SelectList sel = new SelectList(db.table_students.OrderBy(p => p.student_name).Where(p=>p.status=="Active").Select(p => p.student_name).ToList());
            return sel;
        }
        // GET: Result/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_result table_result = db.table_result.Find(id);
            if (table_result == null)
            {
                return HttpNotFound();
            }
            return View(table_result);
        }

        // GET: Result/Create
        public ActionResult Create()
        {
            verify();
           ViewBag.acad_calenda = display_academic_year();
            ViewBag.Semester = display_semester();
            ViewBag.Cours = display_course();
            ViewBag.student_nam = display_student();
            return View();
        }

        // POST: Result/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,Academic_Calender,Semester,Student_Name,Matric_No,Course_Title,Course_Code,Level,Unit,Score,GP,Grade,Remark,Registered_By,Date,Last_Updated,Last_Updated_By,Code,faculty,department")] table_result model)
        {
            ViewBag.acad_calenda = display_academic_year();
            ViewBag.Semester = display_semester();
            ViewBag.Cours = display_course();
            ViewBag.student_nam = display_student();
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Academic_Calender) || string.IsNullOrWhiteSpace(model.Semester) || string.IsNullOrWhiteSpace(model.Course_Title) || string.IsNullOrWhiteSpace(model.Student_Name) || string.IsNullOrWhiteSpace(model.Score.ToString()))
                {
                    ViewBag.Result = "*All Fields Are Required ...";
                }
                else
                {
                    string reg_by = "";string code = model.Student_Name + model.Course_Title + model.Course_Code;
                    var c_code = db.table_course_add.Where(p => p.course == model.Course_Title).Select(p=>p.course_code).FirstOrDefault();
                    var c_unit = db.table_course_add.Where(p => p.course == model.Course_Title).Select(p => p.Unit).FirstOrDefault();
                    var c_level = db.table_course_add.Where(p => p.course == model.Course_Title).Select(p => p.Level).FirstOrDefault();
                    var c_matric = db.table_students.Where(p => p.student_name == model.Student_Name).Select(p=>p.matric_no).FirstOrDefault();
                    var c_faculty = db.table_students.Where(p => p.student_name == model.Student_Name).Select(p => p.faculty).FirstOrDefault();
                    var c_department = db.table_students.Where(p => p.student_name == model.Student_Name).Select(p => p.department).FirstOrDefault();
                    //  ViewBag.Result = c_matric + "" + c_matric;
                    // string course_code = c_code.ToString();string matric_no = c_matric.ToString();
                    db.Database.ExecuteSqlCommand("INSERT INTO Table_Result(Academic_Calender,Semester,Student_Name,Matric_No,Course_Title,Course_Code,Score,Registered_By,Date,Last_Updated_By,Code,Level,Unit,faculty,department)values('" + model.Academic_Calender + "','" + model.Semester + "','" + model.Student_Name + "','" + c_matric + "','" + model.Course_Title + "','" + c_code + "','" + model.Score + "','" + reg_by + "',now(),'" + reg_by + "','" + code + "','" + c_level + "','" + c_unit + "','"+c_faculty+"','"+c_department+"') ON DUPLICATE KEY UPDATE Score=values(Score),Last_Updated_By=values(last_updated_by),level=values(level),unit=values(unit),faculty=values(faculty),department=values(department)");
                    ViewBag.Result = model.Score + " Mark(s) Had Been Successfully Saved for " + model.Student_Name + " For " + model.Course_Title + "(" + c_code + ")";
                }
            }

            return View(model);
        }

        public ActionResult Result_Student()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Result_Student(table_result model)
        {
            return View();
        }
        // GET: Result/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_result table_result = db.table_result.Find(id);
            if (table_result == null)
            {
                return HttpNotFound();
            }
            return View(table_result);
        }

        // POST: Result/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,Academic_Calender,Semester,Student_Name,Matric_No,Course_Title,Course_Code,Level,Score,GP,Grade,Remark,Registered_By,Date,Last_Updated,Last_Updated_By,Code,faculty,department")] table_result model)
        {
            /*if (ModelState.IsValid)
            {
                db.Entry(table_result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/
            if (string.IsNullOrWhiteSpace(model.Academic_Calender) || string.IsNullOrWhiteSpace(model.Semester) || string.IsNullOrWhiteSpace(model.Course_Title) || string.IsNullOrWhiteSpace(model.Student_Name) || string.IsNullOrWhiteSpace(model.Score.ToString()))
            {
                ViewBag.Result = "*Enter Mark/ Score Obtained ...";
            }
            else
            {
                string reg_by = ""; string code = model.Student_Name + model.Course_Title + model.Course_Code;
                var c_code = db.table_course_add.Where(p => p.course == model.Course_Title).Select(p => p.course_code).FirstOrDefault();
                var c_unit = db.table_course_add.Where(p => p.course == model.Course_Title).Select(p => p.Unit).FirstOrDefault();
                var c_level = db.table_course_add.Where(p => p.course == model.Course_Title).Select(p => p.Level).FirstOrDefault();
                var c_matric = db.table_students.Where(p => p.student_name == model.Student_Name).Select(p => p.matric_no).FirstOrDefault();
                var c_faculty = db.table_students.Where(p => p.student_name == model.Student_Name).Select(p => p.faculty).FirstOrDefault();
                var c_department = db.table_students.Where(p => p.student_name == model.Student_Name).Select(p => p.department).FirstOrDefault();
                ViewBag.acad_calenda = display_academic_year();
                db.Database.ExecuteSqlCommand("UPDATE table_result set academic_calender='" + model.Academic_Calender + "',student_name='" + model.Student_Name + "',semester='" + model.Semester + "',matric_no='" + c_matric + "',course_title='" + model.Course_Title + "',course_code='" + c_code + "',Score='" + model.Score + "',unit='" + c_unit + "',level='" + c_level + "',last_updated_by='" + reg_by + "',code='" + code + "',faculty='"+c_faculty+"',department='"+c_department+"' where p_id='" + model.p_id + "'");
                ViewBag.Result = "Update Successfully Saved for " + model.Student_Name + " With Matric No." + c_matric;
            }

            return View(model);
        }

        // GET: Result/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_result table_result = db.table_result.Find(id);
            if (table_result == null)
            {
                return HttpNotFound();
            }
            return View(table_result);
        }

        // POST: Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            table_result table_result = db.table_result.Find(id);
            db.table_result.Remove(table_result);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult fetchStudent() {
            var det = db.table_students.Select(p => p.student_name).ToList();
            return Json(det);
        }

        [HttpPost]
        public JsonResult findResult()
        {
            var rest = db.table_result.SqlQuery("SELECT* from table_result").ToList<table_result>();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult Year() {
            var rest = db.table_calendar_year.OrderBy(p => p.calendar_year).ToList();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult Level_std()
        {
            string mat = (string)Session["matric"];
            var rest = db.table_result.OrderBy(p=>p.Level).Where(p=>p.Matric_No==mat).Distinct().ToList();
            //var rest = db.table_result.SqlQuery("SELECT Distinct Level from table_result where Matric_No='" + mat + "' order by Level").ToList<table_result>();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult Semester_std()
        {
            string mat = (string)Session["matric"];
             var rest = db.table_result.OrderBy(p => p.Semester).Where(p => p.Matric_No == mat).Distinct().ToList();
           // var rest = db.table_result.SqlQuery("SELECT Distinct Semester from table_result where Matric_No='" + mat + "' order by Semester").ToList<table_result>();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult Faculty() {
            var rest = db.table_faculty_add.OrderBy(p => p.faculty).ToList();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult department(string faculty)
        {
            var rest = db.table_department_add.OrderBy(p => p.faculty).Where(p=>p.faculty==faculty).ToList();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult Semester()
        {
            var rest = db.table_semester_add.OrderBy(p => p.Semester).ToList();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult Course()
        {
            var rest = db.table_course_add.OrderBy(p => p.course).ToList();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult Name()
        {
            var rest = db.table_students.OrderBy(p => p.student_name).ToList();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult result_semester_std(string level, string semester)
        {
            string mat = (string)Session["matric"];
            var rest = db.table_result.SqlQuery("SELECT* FROM table_result where level='" + level + "' and semester='" + semester + "' and matric_no='" + mat + "'").ToList<table_result>();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult result_level_std(string level)
        {
            string mat = (string)Session["matric"];
            var rest = db.table_result.SqlQuery("SELECT* FROM table_result where level='" + level + "' and matric_no='" + mat + "'").ToList<table_result>();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult result_semester(string year,string faculty,string department,string semester)
        {
           // return year + faculty + department + semester;
            var rest = db.table_result.SqlQuery("SELECT* FROM table_result where academic_calender='" + year + "' and faculty='"+faculty+"' and department='"+department+"' and semester='" + semester + "'").ToList<table_result>();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult result_course(string year, string faculty, string department, string semester,string course)
        {
            // return year + faculty + department + semester;
            var rest = db.table_result.SqlQuery("SELECT* FROM table_result where academic_calender='" + year + "' and faculty='" + faculty + "' and department='" + department + "' and semester='" + semester + "' and course_title='"+course+"'").ToList<table_result>();
            return Json(rest);
        }

        [HttpPost]
        public JsonResult result_name(string year, string faculty, string department, string semester,string name)
        {
            // return year + faculty + department + semester;
            var rest = db.table_result.SqlQuery("SELECT* FROM table_result where academic_calender='" + year + "' and faculty='" + faculty + "' and department='" + department + "' and semester='" + semester + "' and student_name='"+name+"'").ToList<table_result>();
            return Json(rest);
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
