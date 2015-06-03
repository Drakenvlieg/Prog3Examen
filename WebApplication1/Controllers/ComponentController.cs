using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEFOpdracht.Models;

namespace WebApplication1.Controllers
{
    public class ComponentController : Controller
    {
        private ComponentContext db = new ComponentContext();

        // GET: /Component/
        public ActionResult Index(string sortOrder, string searchString, string SelectedCategory)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CategorieSortParm = sortOrder == "category" ? "category_desc" : "category";
            var componenten = from c in db.Componenten select c;

            //filter op categorie:
            ViewBag.SelectedCategory = new SelectList(componenten.Select(x => x.Categorie).Distinct(), SelectedCategory);
            if (!string.IsNullOrEmpty(SelectedCategory))
            {
                componenten = componenten.Where(x => x.Categorie == SelectedCategory);
            }

            //filter op zoekopdracht:
            if (!String.IsNullOrEmpty(searchString))
            {
                componenten = componenten.Where(c => c.Naam.ToUpper().Contains(searchString.ToUpper()));

            }

            //sorteren:
            switch (sortOrder)
            {
                case "name_desc":
                    componenten = componenten.OrderByDescending(c => c.Naam);
                    break;
                case "category_desc":
                    componenten = componenten.OrderByDescending(c => c.Categorie);
                    break;
                case "category":
                    componenten = componenten.OrderBy(c => c.Categorie);
                    break;

                default:
                    componenten = componenten.OrderBy(c => c.Naam);
                    break;


            }

            return View(componenten.ToList());
        }

        // GET: /Component/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Componenten.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        [Authorize]
        // GET: /Component/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        // POST: /Component/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Categorie,Naam,DatasheetLink,Aankoopprijs,Aantal")] Component component)
        {
            if (ModelState.IsValid)
            {
                db.Componenten.Add(component);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(component);
        }

        [Authorize]
        // GET: /Component/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Componenten.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        [Authorize]
        // POST: /Component/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Categorie,Naam,DatasheetLink,Aankoopprijs,Aantal")] Component component)
        {
            if (ModelState.IsValid)
            {
                db.Entry(component).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(component);
        }

        [Authorize]
        // GET: /Component/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Componenten.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        [Authorize]
        // POST: /Component/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Component component = db.Componenten.Find(id);
            db.Componenten.Remove(component);
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
