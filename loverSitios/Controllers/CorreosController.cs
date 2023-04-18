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
    public class CorreosController : Controller
    {
        private tiusr3pl_loverEntities3 db = new tiusr3pl_loverEntities3();


        public ActionResult Servidor()
        {
            return View();
        }


        // GET: Correos
        public ActionResult Index()
        {
            var correos = db.Correos.Include(c => c.ConfCorreos);
            return View(correos.ToList());
        }

        // GET: Correos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correos correos = db.Correos.Find(id);
            if (correos == null)
            {
                return HttpNotFound();
            }
            return View(correos);
        }

        // GET: Correos/Create
        public ActionResult Create()
        {
            ViewBag.idConfCorreo = new SelectList(db.ConfCorreos, "idConfCorreo", "Servidor");
            return View();
        }

        // POST: Correos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCorreo,Asunto,Mensaje,idConfCorreo")] Correos correos)
        {
            if (ModelState.IsValid)
            {
                db.Correos.Add(correos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idConfCorreo = new SelectList(db.ConfCorreos, "idConfCorreo", "Servidor", correos.idConfCorreo);
            return View(correos);
        }

        // GET: Correos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correos correos = db.Correos.Find(id);
            if (correos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idConfCorreo = new SelectList(db.ConfCorreos, "idConfCorreo", "Servidor", correos.idConfCorreo);
            return View(correos);
        }

        // POST: Correos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCorreo,Asunto,Mensaje,idConfCorreo")] Correos correos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(correos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idConfCorreo = new SelectList(db.ConfCorreos, "idConfCorreo", "Servidor", correos.idConfCorreo);
            return View(correos);
        }

        // GET: Correos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correos correos = db.Correos.Find(id);
            if (correos == null)
            {
                return HttpNotFound();
            }
            return View(correos);
        }

        // POST: Correos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Correos correos = db.Correos.Find(id);
            db.Correos.Remove(correos);
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
