using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecetarioMVC1.Models;

namespace RecetarioMVC1.Controllers
{
    public class platoesController : Controller
    {
        private recetarioEntities db = new recetarioEntities();

        // GET: platoes
        public ActionResult Index()
        {
            var plato = db.plato.Include(p => p.receta);
            return View(plato.ToList());
        }

        // GET: platoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = db.plato.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            return View(plato);
        }

        // GET: platoes/Create
        public ActionResult Create()
        {
            ViewBag.cod_receta = new SelectList(db.receta, "cod_receta", "fuente_receta");
            return View();
        }

        // POST: platoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_plato,cod_receta,tipo_plato,ingredientes_principal,precio,comentario,nombre,calorias,cant_ingredientes,porcion,activo")] plato plato)
        {
            if (ModelState.IsValid)
            {
                db.plato.Add(plato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cod_receta = new SelectList(db.receta, "cod_receta", "fuente_receta", plato.cod_receta);
            return View(plato);
        }

        // GET: platoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = db.plato.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            ViewBag.cod_receta = new SelectList(db.receta, "cod_receta", "fuente_receta", plato.cod_receta);
            return View(plato);
        }

        // POST: platoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_plato,cod_receta,tipo_plato,ingredientes_principal,precio,comentario,nombre,calorias,cant_ingredientes,porcion,activo")] plato plato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cod_receta = new SelectList(db.receta, "cod_receta", "fuente_receta", plato.cod_receta);
            return View(plato);
        }

        // GET: platoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = db.plato.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            return View(plato);
        }

        // POST: platoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            plato plato = db.plato.Find(id);
            db.plato.Remove(plato);
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
