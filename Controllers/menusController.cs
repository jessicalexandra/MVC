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
    public class menusController : Controller
    {
        private recetarioEntities db = new recetarioEntities();

        // GET: menus
        public ActionResult Index()
        {
            var menu = db.menu.Include(m => m.receta);
            return View(menu.ToList());
        }

        // GET: menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu menu = db.menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: menus/Create
        public ActionResult Create()
        {
            ViewBag.cod_receta = new SelectList(db.receta, "cod_receta", "fuente_receta");
            return View();
        }

        // POST: menus/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_menu,cod_receta,nombre_menu,precio_menu,comentario_menu,activo")] menu menu)
        {
            if (ModelState.IsValid)
            {
                db.menu.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cod_receta = new SelectList(db.receta, "cod_receta", "fuente_receta", menu.cod_receta);
            return View(menu);
        }

        // GET: menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu menu = db.menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.cod_receta = new SelectList(db.receta, "cod_receta", "fuente_receta", menu.cod_receta);
            return View(menu);
        }

        // POST: menus/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_menu,cod_receta,nombre_menu,precio_menu,comentario_menu,activo")] menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cod_receta = new SelectList(db.receta, "cod_receta", "fuente_receta", menu.cod_receta);
            return View(menu);
        }

        // GET: menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu menu = db.menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            menu menu = db.menu.Find(id);
            db.menu.Remove(menu);
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
