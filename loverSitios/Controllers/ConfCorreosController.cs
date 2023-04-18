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
    public class ConfCorreosController : Controller
    {
        private tiusr3pl_loverEntities3 db = new tiusr3pl_loverEntities3();

        // GET: ConfCorreos
        public ActionResult Index()
        {
            return View(db.ConfCorreos.ToList());
        }

        // GET: ConfCorreos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfCorreos confCorreos = db.ConfCorreos.Find(id);
            if (confCorreos == null)
            {
                return HttpNotFound();
            }
            return View(confCorreos);
        }

        // GET: ConfCorreos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConfCorreos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idConfCorreo,Servidor,Puerto,Usuario,Contraseña")] ConfCorreos confCorreos)
        {
            if (ModelState.IsValid)
            {
                db.ConfCorreos.Add(confCorreos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(confCorreos);
        }

        // GET: ConfCorreos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfCorreos confCorreos = db.ConfCorreos.Find(id);
            if (confCorreos == null)
            {
                return HttpNotFound();
            }
            return View(confCorreos);
        }

        // POST: ConfCorreos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idConfCorreo,Servidor,Puerto,Usuario,Contraseña")] ConfCorreos confCorreos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(confCorreos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(confCorreos);
        }

        // GET: ConfCorreos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfCorreos confCorreos = db.ConfCorreos.Find(id);
            if (confCorreos == null)
            {
                return HttpNotFound();
            }
            return View(confCorreos);
        }

        // POST: ConfCorreos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConfCorreos confCorreos = db.ConfCorreos.Find(id);
            db.ConfCorreos.Remove(confCorreos);
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
