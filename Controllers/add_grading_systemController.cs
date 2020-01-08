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
    public class add_grading_systemController : Controller
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
        // GET: add_grading_system
        public ActionResult Index()
        {
            verify();
            return View(db.table_grading_system.ToList());
        }

        // GET: add_grading_system/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_grading_system table_grading_system = db.table_grading_system.Find(id);
            if (table_grading_system == null)
            {
                return HttpNotFound();
            }
            return View(table_grading_system);
        }

        // GET: add_grading_system/Create
        public ActionResult Create()
        {
            verify();
            return View();
        }

        // POST: add_grading_system/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,from1,to1,grade,gp1,gp2,remark,code,classification,registered_by,date,last_updated")] table_grading_system model)
        {
            if (string.IsNullOrWhiteSpace(model.from1.ToString()) || string.IsNullOrWhiteSpace(model.to1.ToString()) || string.IsNullOrWhiteSpace(model.gp1.ToString()) || string.IsNullOrWhiteSpace(model.gp2.ToString()) || string.IsNullOrWhiteSpace(model.grade) || string.IsNullOrWhiteSpace(model.remark) || string.IsNullOrWhiteSpace(model.classification))
            {
                ViewBag.Result = "*All Fields Are Required ...";
            }
            else
            {
                string code = (model.from1.ToString() + model.to1.ToString());
                string reg_by = "";
                db.Database.ExecuteSqlCommand("INSERT INTO table_grading_system(from1,to1,grade,gp1,gp2,remark,code,classification,registered_by,date)values('" + model.from1 + "','" + model.to1 + "','" + model.grade + "','" + model.gp1 + "','" + model.gp2 + "','" + model.remark + "','" + code + "','" + model.classification + "','" + reg_by + "',now()) on duplicate key update to1=values(to1),from1=values(from1),grade=values(grade),gp1=values(gp1),gp2=values(gp2),remark=values(remark),classification=values(classification),registered_by=values(registered_by)");
                ViewBag.Result = "Grading System Successfully Added ...";
            }
            return View(model);
        }

        // GET: add_grading_system/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_grading_system table_grading_system = db.table_grading_system.Find(id);
            if (table_grading_system == null)
            {
                return HttpNotFound();
            }
            return View(table_grading_system);
        }

        // POST: add_grading_system/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,from1,to1,grade,gp,remark,code,classification,registered_by,date,last_updated")] table_grading_system table_grading_system)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(table_grading_system).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(table_grading_system);
            }
            catch (Exception ex)
            {
                ViewBag.Result = ex.Message;
            }
            return View();
        }

        // GET: add_grading_system/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_grading_system table_grading_system = db.table_grading_system.Find(id);
            if (table_grading_system == null)
            {
                return HttpNotFound();
            }
            return View(table_grading_system);
        }

        // POST: add_grading_system/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* table_grading_system table_grading_system = db.table_grading_system.Find(id);
             db.table_grading_system.Remove(table_grading_system);
             db.SaveChanges();
             return RedirectToAction("Index");*/
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM table_grading_system where p_id='" + id + "'");
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
