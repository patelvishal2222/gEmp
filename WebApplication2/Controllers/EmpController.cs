using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmpController : Controller
    {
        private Bemp db = new Bemp();

        // GET: /Emp/
        public ActionResult Index()
        {
            return View(db.ToList());
        }

        // GET: /Emp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emp emp = db.Find(id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        // GET: /Emp/Create
        public ActionResult Create()
        {
            BCity  city=new BCity();
            ViewBag.CityId = new SelectList(city.ToList(), "CityId", "CityName");
            return View();
        }

        // POST: /Emp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Emp emp)
        {
            if (ModelState.IsValid)
            {
                
                db.insertUpdate(emp);
                return RedirectToAction("Index");
            }

            return View(emp);
        }

        // GET: /Emp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emp emp = db.Find(id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            BCity city = new BCity();
            ViewBag.CityId = new SelectList(city.ToList(), "CityId", "CityName",emp.CityId);
            return View(emp);
        }

        // POST: /Emp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Emp emp)
        {
            if (ModelState.IsValid)
            {
                db.insertUpdate(emp);
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        // GET: /Emp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emp emp = db.Find(id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        // POST: /Emp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            db.Delete(id);
            return RedirectToAction("Index");
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
