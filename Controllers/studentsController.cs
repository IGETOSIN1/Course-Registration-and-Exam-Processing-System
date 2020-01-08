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
    public class studentsController : Controller
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
        public List<SelectListItem> display_sex()
       {
            List<SelectListItem> list = new List<SelectListItem>()
           {
               new SelectListItem {Text="Male",Value="Male" },
               new SelectListItem {Text="Female",Value="Female" },
                         };
           return list;
       }
        public List<SelectListItem> display_marital_status()
        {
            List<SelectListItem> list = new List<SelectListItem>()
           {
               new SelectListItem {Text="Single",Value="Single" },
               new SelectListItem {Text="Married",Value="Married" },
               new SelectListItem {Text="Divorced",Value="Divorced" },
                         };
            return list;
        }
        public SelectList display_faculty() {
            SelectList sel = new SelectList(db.table_faculty_add.OrderBy(p => p.faculty).Select(p => p.faculty).ToList());
            return sel;
        }
        public SelectList display_department()
        {
            SelectList sel = new SelectList(db.table_department_add.OrderBy(p => p.department).Select(p => p.department).ToList());
            return sel;
        }
        public SelectList display_program()
        {
            SelectList sel = new SelectList(db.table_program_add.OrderBy(p => p.program).Select(p => p.program).ToList());
            return sel;
        }
        public SelectList display_mode()
        {
            SelectList sel = new SelectList(db.table_mode_add.OrderBy(p => p.mode).Select(p => p.mode).ToList());
            return sel;
        }
        public SelectList display_campus()
        {
            SelectList sel = new SelectList(db.table_campus_add.OrderBy(p => p.campus).Select(p => p.campus).ToList());
            return sel;
        }
        public SelectList display_status()
        {
            SelectList sel = new SelectList(db.table_status_add.OrderBy(p => p.status).Select(p => p.status).ToList());
            return sel;
        }

        // GET: students
        public ActionResult Index()
        {
            verify();
            return View(db.table_students.OrderBy(p=>p.student_name).ToList());
        }

        public ActionResult Index_Student()
        {
            verify();
            string matric_no = (string)Session["matric"];
            return View(db.table_students.OrderBy(p => p.student_name).Where(p=>p.matric_no==matric_no).ToList());
        }

        // GET: students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_students table_students = db.table_students.Find(id);
            if (table_students == null)
            {
                return HttpNotFound();
            }
            return View(table_students);
        }

        public ActionResult Details_Student(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_students table_students = db.table_students.Find(id);
            if (table_students == null)
            {
                return HttpNotFound();
            }
            return View(table_students);
        }

        [HttpGet]
        public ActionResult edit_student_name()
        {
            verify();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit_student_name([Bind(Include = "p_id,first_name,last_name,student_name,matric_no,date_of_birth,place_of_birth,sex,nationality,marital_status,age,phone,email,code,sponsor_name,sponsor_address,sponsor_relationship,sponsor_occupation,sponsor_phone,sponsor_email,highest_qualification,qualification,year_of_admission,faculty,department,program,mode,campus,note,status,registered_by,date,dat")] table_students model)
        {
            if (string.IsNullOrWhiteSpace(model.first_name))
            {
                ViewBag.Result = "Enter Student's First Name/ Other Name(s) ...";
            }
            else if (string.IsNullOrWhiteSpace(model.last_name))
            {
                ViewBag.Result = "Enter Student's Surname/ Last Name ...";
            }
            else if (string.IsNullOrWhiteSpace(model.matric_no))
            {
                ViewBag.Result = "Enter Student's Matric/ Admission ID ...";
            }
            else
            {
                string student_name = model.last_name + " " + model.first_name;
                db.Database.ExecuteSqlCommand("Update table_students set first_name='"+model.first_name+"',last_name='"+model.last_name+"',student_name='" + student_name + "' where matric_no='" + model.matric_no + "';Update table_user set  first_name='" + model.first_name + "',last_name='" + model.last_name + "',full_name='" + student_name + "' where matric_no='" + model.matric_no + "'");
                ViewBag.Result = "Name Update/Edit Successfully Executed ...";
            }
            return View();
        }

        public ActionResult edit_student_id()
        {
            verify();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit_student_id([Bind(Include = "p_id,first_name,last_name,student_name,matric_no,date_of_birth,place_of_birth,sex,nationality,marital_status,age,phone,email,code,sponsor_name,sponsor_address,sponsor_relationship,sponsor_occupation,sponsor_phone,sponsor_email,highest_qualification,qualification,year_of_admission,faculty,department,program,mode,campus,note,status,registered_by,date,dat")] FormCollection fmc, table_students model)
        {
            if (string.IsNullOrWhiteSpace(model.matric_no))
            {
                ViewBag.Result = "Enter Student's Current Matric No/ Student's ID ...";
            }
            else if (string.IsNullOrWhiteSpace(fmc["new_matric"]))
            {
                ViewBag.Result = "Enter Student's New Matric No/ Student's ID ...";
            }
            else if (string.IsNullOrWhiteSpace(fmc["confirm_new_matric"]))
            {
                ViewBag.Result = "Confirm New Matric No/ Student's ID ...";
            }
            else if (fmc["new_matric"] != fmc["confirm_new_matric"])
            {
                ViewBag.Result = "New Matric No/ Students ID With Confirm Does Not Match ...";
            }
            else
            {
                string mat = fmc["new_matric"];
                var rest = db.table_students.Where(p => p.matric_no == mat).FirstOrDefault();
                if (rest != null)
                {
                    ViewBag.Result = "Matric No/Student's ID (" + fmc["new_matric"] + ") Already Exists ...";
                }
                else
                {
                    string student_name = model.last_name + " " + model.first_name;
                    db.Database.ExecuteSqlCommand("Update table_students set matric_no='" + fmc["new_matric"] + "' where matric_no='" + model.matric_no + "';Update table_user set matric_no='" + fmc["new_matric"] + "' where matric_no='" + model.matric_no + "'");
                    ViewBag.Result = "Matric No./ student's ID Successfully Changed from " + model.matric_no + " to " + fmc["new_matric"]; //"Update/Edit Successfully Executed ...";
                }
            }
            return View();
        }

        // GET: students/Create
        public ActionResult Create()
        {
            verify();
            ViewBag.se = display_sex();
            ViewBag.marital_statu = display_marital_status();
            ViewBag.facult = display_faculty();
            ViewBag.departmen = display_department();
            ViewBag.progra = display_program();
            ViewBag.campu = display_campus();
            ViewBag.mod = display_mode();
            ViewBag.statu = display_status();
            return View();
        }

        // POST: students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,first_name,last_name,student_name,matric_no,date_of_birth,place_of_birth,sex,nationality,marital_status,age,phone,email,code,sponsor_name,sponsor_address,sponsor_relationship,sponsor_occupation,sponsor_phone,sponsor_email,highest_qualification,qualification,year_of_admission,faculty,department,program,mode,campus,note,status,registered_by,date,dat")] table_students model)
        {
            ViewBag.se = display_sex();
            ViewBag.marital_statu = display_marital_status();
            ViewBag.facult = display_faculty();
            ViewBag.departmen = display_department();
            ViewBag.progra = display_program();
            ViewBag.campu = display_campus();
            ViewBag.mod = display_mode();
            ViewBag.statu = display_status();
            if (string.IsNullOrWhiteSpace(model.first_name))
            {
                ViewBag.Result = "*Enter First/ Given Name(s) ...";
            }
            else if (string.IsNullOrWhiteSpace(model.first_name))
            {
                ViewBag.Result = "*Enter Last/ Surname ...";
            }
            else if (string.IsNullOrWhiteSpace(model.date_of_birth))
            {
                ViewBag.Result = "*Enter Date of Birth ...";
            }
            else if (string.IsNullOrWhiteSpace(model.matric_no))
            {
                ViewBag.Result = "*Enter Matric/ Admission No. For The Student ...";
            }
            else if (string.IsNullOrWhiteSpace(model.faculty))
            {
                ViewBag.Result = "*Select Faculty/ School From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.department))
            {
                ViewBag.Result = "*Select Department From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.program))
            {
                ViewBag.Result = "*Select Program From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.mode))
            {
                ViewBag.Result = "*Select Mode of Study From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.campus))
            {
                ViewBag.Result = "*Select Campus From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.status))
            {
                ViewBag.Result = "*Select Status From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.sex) || string.IsNullOrWhiteSpace(model.nationality) || string.IsNullOrWhiteSpace(model.age) || string.IsNullOrWhiteSpace(model.highest_qualification))
            {
                ViewBag.Result = "*Enter/ Select Sex, Nationality, Age, and Highest Qualification ...";
            }
            else
            {
                var search = db.table_students.Where(p => p.matric_no == model.matric_no).FirstOrDefault();
                if (search == null)
                {
                    Random rnd = new Random();
                    string ast = rnd.Next(00000123, 98765432).ToString();
                    string reg_by = "";
                    string code = model.last_name + model.first_name + model.date_of_birth + model.program;
                    string cd = model.last_name + model.first_name + model.date_of_birth + model.program + ast;
                    string student_name = model.last_name + " " + model.first_name;
                    db.Database.ExecuteSqlCommand("INSERT INTO table_students(first_name,last_name,student_name,matric_no,date_of_birth,place_of_birth,sex,nationality,marital_status,age,phone,email,code,sponsor_name,sponsor_address,sponsor_relationship,sponsor_occupation,sponsor_phone,sponsor_email,highest_qualification,qualification,year_of_admission,faculty,department,program,mode,campus,note,status,registered_by,date)values('" + model.first_name + "','" + model.last_name + "','" + student_name + "','" + model.matric_no + "','" + model.date_of_birth + "','" + model.place_of_birth + "','" + model.sex + "','" + model.nationality + "','" + model.marital_status + "','" + model.age + "','" + model.phone + "','" + model.email + "','" + code + "','" + model.sponsor_name + "','" + model.sponsor_address + "','" + model.sponsor_relationship + "','" + model.sponsor_occupation + "','" + model.sponsor_phone + "','" + model.sponsor_email + "','" + model.highest_qualification + "','" + model.qualification + "','" + model.year_of_admission + "','" + model.faculty + "','" + model.department + "','" + model.program + "','" + model.mode + "','" + model.campus + "','" + model.note + "','" + model.status + "','" + reg_by + "',now()) on duplicate key update sex=values(sex);insert into table_user(first_name,last_name,full_name,matric_no,user_name,password,role,date)values('"+model.first_name+ "','" + model.last_name + "','" + student_name + "','" + model.matric_no + "','" + cd + "','escae2019','Student',now()) on duplicate key update date=values(date);");
                    ViewBag.Result = student_name + " Had Been Successfully Registered ...";
                }
                else
                {
                    ViewBag.Result = "Matric/ Admission Number (" + model.matric_no + ") Already Exists/ Allocated ...";
                }

            }
            return View(model);
        }

        // GET: students/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            ViewBag.se = display_sex();
            ViewBag.marital_statu = display_marital_status();
            ViewBag.facult = display_faculty();
            ViewBag.departmen = display_department();
            ViewBag.progra = display_program();
            ViewBag.campu = display_campus();
            ViewBag.mod = display_mode();
            ViewBag.statu = display_status();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_students table_students = db.table_students.Find(id);
            if (table_students == null)
            {
                return HttpNotFound();
            }
            return View(table_students);
        }

        // POST: students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,first_name,last_name,student_name,matric_no,date_of_birth,place_of_birth,sex,nationality,marital_status,age,phone,email,code,sponsor_name,sponsor_address,sponsor_relationship,sponsor_occupation,sponsor_phone,sponsor_email,highest_qualification,qualification,year_of_admission,faculty,department,program,mode,campus,note,status,registered_by,date,dat")] table_students model)
        {
            ViewBag.se = display_sex();
            ViewBag.marital_statu = display_marital_status();
            ViewBag.facult = display_faculty();
            ViewBag.departmen = display_department();
            ViewBag.progra = display_program();
            ViewBag.campu = display_campus();
            ViewBag.mod = display_mode();
            ViewBag.statu = display_status();
            if (string.IsNullOrWhiteSpace(model.first_name))
            {
                ViewBag.Result = "*Enter First/ Given Name(s) ...";
            }
            else if (string.IsNullOrWhiteSpace(model.first_name))
            {
                ViewBag.Result = "*Enter Last/ Surname ...";
            }
            else if (string.IsNullOrWhiteSpace(model.date_of_birth))
            {
                ViewBag.Result = "*Enter Date of Birth ...";
            }
            else if (string.IsNullOrWhiteSpace(model.matric_no))
            {
                ViewBag.Result = "*Enter Matric/ Admission No. For The Student ...";
            }
            else if (string.IsNullOrWhiteSpace(model.faculty))
            {
                ViewBag.Result = "*Select Faculty/ School From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.department))
            {
                ViewBag.Result = "*Select Department From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.program))
            {
                ViewBag.Result = "*Select Program From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.mode))
            {
                ViewBag.Result = "*Select Mode of Study From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.campus))
            {
                ViewBag.Result = "*Select Campus From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.status))
            {
                ViewBag.Result = "*Select Status From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.sex) || string.IsNullOrWhiteSpace(model.nationality) || string.IsNullOrWhiteSpace(model.age) || string.IsNullOrWhiteSpace(model.highest_qualification))
            {
                ViewBag.Result = "*Enter/ Select Sex, Nationality, Age, and Highest Qualification ...";
            }
            else
            {
                try
                {
                    string reg_by = "";
                    string code = model.last_name + model.first_name + model.date_of_birth + model.program;
                    string student_name = model.last_name + " " + model.first_name;
                    // db.Database.ExecuteSqlCommand("INSERT INTO table_students(first_name,last_name,student_name,matric_no,date_of_birth,place_of_birth,sex,nationality,marital_status,age,phone,email,code,sponsor_name,sponsor_address,sponsor_relationship,sponsor_occupation,sponsor_phone,sponsor_email,highest_qualification,qualification,year_of_admission,faculty,department,program,mode,campus,note,status,registered_by,date)values('" + model.first_name + "','" + model.last_name + "','" + student_name + "','" + model.matric_no + "','" + model.date_of_birth + "','" + model.place_of_birth + "','" + model.sex + "','" + model.nationality + "','" + model.marital_status + "','" + model.age + "','" + model.phone + "','" + model.email + "','" + code + "','" + model.sponsor_name + "','" + model.sponsor_address + "','" + model.sponsor_relationship + "','" + model.sponsor_occupation + "','" + model.sponsor_phone + "','" + model.sponsor_email + "','" + model.highest_qualification + "','" + model.qualification + "','" + model.year_of_admission + "','" + model.faculty + "','" + model.department + "','" + model.program + "','" + model.mode + "','" + model.campus + "','" + model.note + "','" + model.status + "','" + reg_by + "',now()) on duplicate key update sex=values(sex)");
                    db.Database.ExecuteSqlCommand("update table_students set first_name='" + model.first_name + "',last_name='" + model.last_name + "',student_name='" + student_name + "',date_of_birth='" + model.date_of_birth + "',place_of_birth='" + model.place_of_birth + "',sex='" + model.sex + "',nationality='" + model.nationality + "',marital_status='" + model.marital_status + "',age='" + model.age + "',phone='" + model.phone + "',email='" + model.email + "',code='" + code + "',sponsor_name='" + model.sponsor_name + "',sponsor_address='" + model.sponsor_address + "',sponsor_occupation='" + model.sponsor_occupation + "',sponsor_relationship='" + model.sponsor_relationship + "',sponsor_phone='" + model.sponsor_phone + "',sponsor_email='" + model.sponsor_email + "',highest_qualification='" + model.highest_qualification + "',qualification='" + model.qualification + "',year_of_admission='" + model.year_of_admission + "',faculty='" + model.faculty + "',department='" + model.department + "',program='" + model.program + "',mode='" + model.mode + "',campus='" + model.campus + "',note='" + model.note + "',status='" + model.status + "',registered_by='" + reg_by + "' where p_id='" + model.p_id + "';update table_user set first_name='" + model.first_name + "',last_name='" + model.last_name + "',full_name='" + student_name + "' where matric_no='" + model.matric_no + "';");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            return View(model);
        }

        public ActionResult Edit_Student(int? id)
        {
            verify();
            ViewBag.se = display_sex();
            ViewBag.marital_statu = display_marital_status();
            ViewBag.facult = display_faculty();
            ViewBag.departmen = display_department();
            ViewBag.progra = display_program();
            ViewBag.campu = display_campus();
            ViewBag.mod = display_mode();
            ViewBag.statu = display_status();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_students table_students = db.table_students.Find(id);
            if (table_students == null)
            {
                return HttpNotFound();
            }
            return View(table_students);
        }

        // POST: students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Student([Bind(Include = "p_id,first_name,last_name,student_name,matric_no,date_of_birth,place_of_birth,sex,nationality,marital_status,age,phone,email,code,sponsor_name,sponsor_address,sponsor_relationship,sponsor_occupation,sponsor_phone,sponsor_email,highest_qualification,qualification,year_of_admission,faculty,department,program,mode,campus,note,status,registered_by,date,dat")] table_students model)
        {
            ViewBag.se = display_sex();
            ViewBag.marital_statu = display_marital_status();
            ViewBag.facult = display_faculty();
            ViewBag.departmen = display_department();
            ViewBag.progra = display_program();
            ViewBag.campu = display_campus();
            ViewBag.mod = display_mode();
            ViewBag.statu = display_status();
            if (string.IsNullOrWhiteSpace(model.first_name))
            {
                ViewBag.Result = "*Enter First/ Given Name(s) ...";
            }
            else if (string.IsNullOrWhiteSpace(model.first_name))
            {
                ViewBag.Result = "*Enter Last/ Surname ...";
            }
            else if (string.IsNullOrWhiteSpace(model.date_of_birth))
            {
                ViewBag.Result = "*Enter Date of Birth ...";
            }
            else if (string.IsNullOrWhiteSpace(model.matric_no))
            {
                ViewBag.Result = "*Enter Matric/ Admission No. For The Student ...";
            }
            else if (string.IsNullOrWhiteSpace(model.faculty))
            {
                ViewBag.Result = "*Select Faculty/ School From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.department))
            {
                ViewBag.Result = "*Select Department From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.program))
            {
                ViewBag.Result = "*Select Program From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.mode))
            {
                ViewBag.Result = "*Select Mode of Study From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.campus))
            {
                ViewBag.Result = "*Select Campus From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.status))
            {
                ViewBag.Result = "*Select Status From List ...";
            }
            else if (string.IsNullOrWhiteSpace(model.sex) || string.IsNullOrWhiteSpace(model.nationality) || string.IsNullOrWhiteSpace(model.age) || string.IsNullOrWhiteSpace(model.highest_qualification))
            {
                ViewBag.Result = "*Enter/ Select Sex, Nationality, Age, and Highest Qualification ...";
            }
            else
            {
                try
                {
                    string reg_by = "";
                    string code = model.last_name + model.first_name + model.date_of_birth + model.program;
                    string student_name = model.last_name + " " + model.first_name;
                    // db.Database.ExecuteSqlCommand("INSERT INTO table_students(first_name,last_name,student_name,matric_no,date_of_birth,place_of_birth,sex,nationality,marital_status,age,phone,email,code,sponsor_name,sponsor_address,sponsor_relationship,sponsor_occupation,sponsor_phone,sponsor_email,highest_qualification,qualification,year_of_admission,faculty,department,program,mode,campus,note,status,registered_by,date)values('" + model.first_name + "','" + model.last_name + "','" + student_name + "','" + model.matric_no + "','" + model.date_of_birth + "','" + model.place_of_birth + "','" + model.sex + "','" + model.nationality + "','" + model.marital_status + "','" + model.age + "','" + model.phone + "','" + model.email + "','" + code + "','" + model.sponsor_name + "','" + model.sponsor_address + "','" + model.sponsor_relationship + "','" + model.sponsor_occupation + "','" + model.sponsor_phone + "','" + model.sponsor_email + "','" + model.highest_qualification + "','" + model.qualification + "','" + model.year_of_admission + "','" + model.faculty + "','" + model.department + "','" + model.program + "','" + model.mode + "','" + model.campus + "','" + model.note + "','" + model.status + "','" + reg_by + "',now()) on duplicate key update sex=values(sex)");
                    db.Database.ExecuteSqlCommand("update table_students set first_name='" + model.first_name + "',last_name='" + model.last_name + "',student_name='" + student_name + "',date_of_birth='" + model.date_of_birth + "',place_of_birth='" + model.place_of_birth + "',sex='" + model.sex + "',nationality='" + model.nationality + "',marital_status='" + model.marital_status + "',age='" + model.age + "',phone='" + model.phone + "',email='" + model.email + "',code='" + code + "',sponsor_name='" + model.sponsor_name + "',sponsor_address='" + model.sponsor_address + "',sponsor_occupation='" + model.sponsor_occupation + "',sponsor_relationship='" + model.sponsor_relationship + "',sponsor_phone='" + model.sponsor_phone + "',sponsor_email='" + model.sponsor_email + "',highest_qualification='" + model.highest_qualification + "',qualification='" + model.qualification + "',year_of_admission='" + model.year_of_admission + "',faculty='" + model.faculty + "',department='" + model.department + "',program='" + model.program + "',mode='" + model.mode + "',campus='" + model.campus + "',note='" + model.note + "',status='" + model.status + "',registered_by='" + reg_by + "' where p_id='" + model.p_id + "';update table_user set first_name='" + model.first_name + "',last_name='" + model.last_name + "',full_name='" + student_name + "' where matric_no='" + model.matric_no + "';");
                    return RedirectToAction("Index_Student");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            return View(model);
        }


        // GET: students/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_students table_students = db.table_students.Find(id);
            if (table_students == null)
            {
                return HttpNotFound();
            }
            return View(table_students);
        }

        // POST: students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /*table_students table_students = db.table_students.Find(id);
            db.table_students.Remove(table_students);
            db.SaveChanges();
            return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_students where p_id='" + id + "'");
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
