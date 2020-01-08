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
    public class userController : Controller
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
        public SelectList display_staff()
        {
            SelectList sel = new SelectList(db.table_staff.OrderBy(p => p.full_name).Select(p => p.full_name).ToList());
            return sel;
        }
        public List<SelectListItem> display_role()
        {
            List<SelectListItem> list = new List<SelectListItem>()
           {
               new SelectListItem {Text="Staff",Value="Staff" },
               new SelectListItem {Text="Admin",Value="Admin" },
             //  new SelectListItem {Text="Divorced",Value="Divorced" },
                         };
            return list;
        }
        // GET: user
        public ActionResult Index()
        {
            verify();
            return View(db.table_user.OrderBy(p => p.full_name).ToList());
        }

        // GET: user/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_user table_user = db.table_user.Find(id);
            if (table_user == null)
            {
                return HttpNotFound();
            }
            return View(table_user);
        }

        // GET: user/Create
        public ActionResult Create()
        {
            verify();
            ViewBag.full_nam = display_staff();
            ViewBag.rol = display_role();
            return View();
        }

        // POST: user/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,first_name,last_name,full_name,matric_no,user_name,password,role,date,dat")] table_user model)
        {
            ViewBag.full_nam = display_staff();
            ViewBag.rol = display_role();
            if (string.IsNullOrWhiteSpace(model.full_name) || string.IsNullOrWhiteSpace(model.user_name) || string.IsNullOrWhiteSpace(model.password) || string.IsNullOrWhiteSpace(model.role))
            {
                ViewBag.Result = "*All Fields Are Required ...";
            }
            else
            {
                var search = db.table_user.Where(p => p.user_name == model.user_name).FirstOrDefault();
                if (search == null)
                {
                    Random rnd = new Random();
                    string ast = rnd.Next(10200145, 98765432).ToString();
                    string reg_by = "";
                    string matric_n = model.full_name + DateTime.Now + ast;
                    db.Database.ExecuteSqlCommand("INSERT INTO table_user(full_name,matric_no,user_name,password,role,date)values('" + model.full_name + "','" + matric_n + "','" + model.user_name + "','" + model.password + "','" + model.role + "',now()) on duplicate key update full_name=values(full_name)");
                    ViewBag.Result = "Account Successfully Created for " + model.full_name + " With User Name: " + model.user_name;
                }
                else
                {
                    ViewBag.Result = "User Name (" + model.user_name + ") Already Exist ...";
                }
            }

            return View(model);
        }

        // GET: user/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
           // ViewBag.full_nam = display_staff();
            ViewBag.rol = display_role();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_user table_user = db.table_user.Find(id);
            if (table_user == null)
            {
                return HttpNotFound();
            }
            return View(table_user);
        }

        // POST: user/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,first_name,last_name,full_name,matric_no,user_name,password,role,date,dat")] table_user model)
        {
            //ViewBag.full_nam = display_staff();
            ViewBag.rol = display_role();
            if (string.IsNullOrWhiteSpace(model.full_name) || string.IsNullOrWhiteSpace(model.user_name) || string.IsNullOrWhiteSpace(model.password) || string.IsNullOrWhiteSpace(model.role))
            {
                ViewBag.Result = "*All Fields Are Required ...";
            }
            else
            {
                try
                {
                    Random rnd = new Random();
                    string ast = rnd.Next(10200145, 98765432).ToString();
                    string reg_by = "";
                    string matric_n = model.full_name + DateTime.Now + ast;
                    db.Database.ExecuteSqlCommand("Update table_user set first_name='" + model.first_name + "',last_name='" + model.last_name + "',full_name='" + model.full_name + "',matric_no='" + model.matric_no + "',role='" + model.role + "' where p_id='" + model.p_id + "'");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }
                /* if (ModelState.IsValid)
                 {
                     db.Entry(table_user).State = EntityState.Modified;
                     db.SaveChanges();
                     return RedirectToAction("Index");
                 }*/
                return View(model);
        }

        // GET: user/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_user table_user = db.table_user.Find(id);
            if (table_user == null)
            {
                return HttpNotFound();
            }
            return View(table_user);
        }

        // POST: user/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* table_user table_user = db.table_user.Find(id);
             db.table_user.Remove(table_user);
             db.SaveChanges();
             return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_user where p_id='" + id + "'");
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
