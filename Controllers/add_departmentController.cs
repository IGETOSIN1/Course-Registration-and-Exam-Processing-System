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
    public class add_departmentController : Controller
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
            SelectList dt = new SelectList(db.table_faculty_add.OrderBy(m => m.faculty).Select(p => p.faculty).Distinct().ToList());
            return dt;
        }
        // GET: add_department
        public ActionResult Index()
        {
            verify();
            return View(db.table_department_add.ToList());
        }
        /*
          public List<SelectListItem> display_session()
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem {Text="2018/2019",Value="2018/2019" },
                new SelectListItem {Text="2019/2020",Value="2019/2020" },
                new SelectListItem {Text="2020/2021",Value="2020/2021" },
                new SelectListItem {Text="2021/2022",Value="2021/2022" },
            };
            return list;
        }
            */
        // GET: add_department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_department_add table_department_add = db.table_department_add.Find(id);
            if (table_department_add == null)
            {
                return HttpNotFound();
            }
            return View(table_department_add);
        }

        // GET: add_department/Create
        public ActionResult Create()
        {
            verify();
            ViewBag.facult = display_faculty();
            return View();
        }

        // POST: add_department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,faculty,department,code,registered_by,date,dat")] table_department_add model)
        {
            /* if (ModelState.IsValid)
             {
                 db.table_department_add.Add(table_department_add);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }*/
            if (string.IsNullOrWhiteSpace(model.faculty) || string.IsNullOrWhiteSpace(model.department))
            {
                ViewBag.Result = "*All Fields Are Required ...";
            }
            else
            {
                string code = model.faculty + model.department;
                db.Database.ExecuteSqlCommand("insert into table_department_add(faculty,department,code,date)values('" + model.faculty + "','" + model.department + "','" + code + "',now())ON DUPLICATE KEY UPDATE faculty=values(faculty),department=values(department),code=values(code)");
                ViewBag.Result = model.department + " Had Been Successfully Added ...";
            }
            ViewBag.facult = display_faculty();
            return View(model);
        }

        // GET: add_department/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            ViewBag.facult = display_faculty();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_department_add table_department_add = db.table_department_add.Find(id);
            if (table_department_add == null)
            {
                return HttpNotFound();
            }
            return View(table_department_add);
        }

        // POST: add_department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,faculty,department,code,registered_by,date,dat")] table_department_add model)
        {
            ViewBag.facult = display_faculty();
            if (string.IsNullOrWhiteSpace(model.faculty) || string.IsNullOrWhiteSpace(model.department))
            {
                ViewBag.Result = "*All Fields Are Required ...";
            }
            else
            {
                try
                {
                    string code = model.faculty + model.department;
                    db.Database.ExecuteSqlCommand("Update table_department_add set faculty='" + model.faculty + "',department='" + model.department + "',code='" + model.code + "' where p_id='" + model.p_id + "'");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
            return View(model);
        }

        // GET: add_department/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_department_add table_department_add = db.table_department_add.Find(id);
            if (table_department_add == null)
            {
                return HttpNotFound();
            }
            return View(table_department_add);
        }

        // POST: add_department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* table_department_add table_department_add = db.table_department_add.Find(id);
             db.table_department_add.Remove(table_department_add);
             db.SaveChanges();
             return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_department_add where p_id='" + id + "'");
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
