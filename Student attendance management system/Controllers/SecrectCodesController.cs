using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Student_attendance_management_system.Models;

namespace Student_attendance_management_system.Controllers
{
    public class SecrectCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SecrectCodes
        public ActionResult Index()
        {
            return View(db.SecrectCodes.ToList());
        }

        // GET: SecrectCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecrectCode secrectCode = db.SecrectCodes.Find(id);
            if (secrectCode == null)
            {
                return HttpNotFound();
            }
            return View(secrectCode);
        }

        // GET: SecrectCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecrectCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SecrectKey")] SecrectCode secrectCode)
        {

            var matchSecrectCode = db.SecrectCodes.SingleOrDefault(x => x.SecrectKey == secrectCode.SecrectKey);
            if (matchSecrectCode != null)
            {
                @ViewBag.Message = "This key was already added try another";
                return View();
            }


            if (ModelState.IsValid)
            {
                db.SecrectCodes.Add(secrectCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(secrectCode);
        }

        // GET: SecrectCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecrectCode secrectCode = db.SecrectCodes.Find(id);
            if (secrectCode == null)
            {
                return HttpNotFound();
            }
            return View(secrectCode);
        }

        // POST: SecrectCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SecrectKey")] SecrectCode secrectCode)
        {



            var matchSecrectCode = db.SecrectCodes.SingleOrDefault(x => x.SecrectKey == secrectCode.SecrectKey);
            if (matchSecrectCode != null)
            {
                @ViewBag.Message = "This key was already added try another";
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Entry(secrectCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(secrectCode);
        }

        // GET: SecrectCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecrectCode secrectCode = db.SecrectCodes.Find(id);
            if (secrectCode == null)
            {
                return HttpNotFound();
            }
            return View(secrectCode);
        }

        // POST: SecrectCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SecrectCode secrectCode = db.SecrectCodes.Find(id);
            db.SecrectCodes.Remove(secrectCode);
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
