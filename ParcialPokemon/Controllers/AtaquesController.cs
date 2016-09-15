using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParcialPokemon.Models;

namespace ParcialPokemon.Controllers
{
    public class AtaquesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ataques
        public ActionResult Index()
        {
            var ataques = db.Ataques.Include(a => a.Pokemon);
            return View(ataques.ToList());
        }

        // GET: Ataques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ataque ataque = db.Ataques.Find(id);
            if (ataque == null)
            {
                return HttpNotFound();
            }
            return View(ataque);
        }

        // GET: Ataques/Create
        public ActionResult Create()
        {
            ViewBag.IdPokemon = new SelectList(db.Pokemones, "IdPokemon", "Nombre");
            return View();
        }

        // POST: Ataques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAtaque,Nombre,Tipo,Nivel,IdPokemon")] Ataque ataque)
        {
            if (ModelState.IsValid)
            {
                db.Ataques.Add(ataque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPokemon = new SelectList(db.Pokemones, "IdPokemon", "Nombre", ataque.IdPokemon);
            return View(ataque);
        }

        // GET: Ataques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ataque ataque = db.Ataques.Find(id);
            if (ataque == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPokemon = new SelectList(db.Pokemones, "IdPokemon", "Nombre", ataque.IdPokemon);
            return View(ataque);
        }

        // POST: Ataques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAtaque,Nombre,Tipo,Nivel,IdPokemon")] Ataque ataque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ataque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPokemon = new SelectList(db.Pokemones, "IdPokemon", "Nombre", ataque.IdPokemon);
            return View(ataque);
        }

        // GET: Ataques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ataque ataque = db.Ataques.Find(id);
            if (ataque == null)
            {
                return HttpNotFound();
            }
            return View(ataque);
        }

        // POST: Ataques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ataque ataque = db.Ataques.Find(id);
            db.Ataques.Remove(ataque);
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
