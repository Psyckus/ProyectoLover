using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using loverSitios.Modelo;

namespace loverSitios.Controllers
{
    public class ConfiguracionsController : Controller
    {
        private tiusr3pl_loverEntities3 db = new tiusr3pl_loverEntities3();

        // GET: Configuracions
        public ActionResult Index()
        {
            return View(db.Configuracion.ToList());
        }

        // GET: Configuracions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuracion configuracion = db.Configuracion.Find(id);
            if (configuracion == null)
            {
                return HttpNotFound();
            }
            return View(configuracion);
        }

        // GET: Configuracions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Configuracions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_configuracion,nombre,LimiteSeleccion")] Configuracion configuracion)
        {
            if (ModelState.IsValid)
            {
                db.Configuracion.Add(configuracion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(configuracion);
        }

        // GET: Configuracions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuracion configuracion = db.Configuracion.Find(id);
            if (configuracion == null)
            {
                return HttpNotFound();
            }
            return View(configuracion);
        }

        // POST: Configuracions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_configuracion,nombre,LimiteSeleccion")] Configuracion configuracion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(configuracion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(configuracion);
        }

        // GET: Configuracions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuracion configuracion = db.Configuracion.Find(id);
            if (configuracion == null)
            {
                return HttpNotFound();
            }
            return View(configuracion);
        }

        // POST: Configuracions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Configuracion configuracion = db.Configuracion.Find(id);
            db.Configuracion.Remove(configuracion);
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
