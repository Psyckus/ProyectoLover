using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using capaPresentacionValidadorPerfiles.Modelo;

namespace capaPresentacionValidadorPerfiles.Controllers
{
    public class ModEmparejamientoesController : Controller
    {
        private tiusr3pl_loverEntities1 db = new tiusr3pl_loverEntities1();

        // GET: ModEmparejamientoes
        public ActionResult Index()
        {
            return View(db.ModEmparejamiento.ToList());
        }

        // GET: ModEmparejamientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModEmparejamiento modEmparejamiento = db.ModEmparejamiento.Find(id);
            if (modEmparejamiento == null)
            {
                return HttpNotFound();
            }
            return View(modEmparejamiento);
        }

        // GET: ModEmparejamientoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModEmparejamientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CriterioInteres,EdadCriterio,TestCriterio,Id_modconf")] ModEmparejamiento modEmparejamiento)
        {
            if (ModelState.IsValid)
            {
                db.ModEmparejamiento.Add(modEmparejamiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modEmparejamiento);
        }

        // GET: ModEmparejamientoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModEmparejamiento modEmparejamiento = db.ModEmparejamiento.Find(id);
            if (modEmparejamiento == null)
            {
                return HttpNotFound();
            }
            return View(modEmparejamiento);
        }

        // POST: ModEmparejamientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CriterioInteres,EdadCriterio,TestCriterio,Id_modconf")] ModEmparejamiento modEmparejamiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modEmparejamiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modEmparejamiento);
        }

        // GET: ModEmparejamientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModEmparejamiento modEmparejamiento = db.ModEmparejamiento.Find(id);
            if (modEmparejamiento == null)
            {
                return HttpNotFound();
            }
            return View(modEmparejamiento);
        }

        // POST: ModEmparejamientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModEmparejamiento modEmparejamiento = db.ModEmparejamiento.Find(id);
            db.ModEmparejamiento.Remove(modEmparejamiento);
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
