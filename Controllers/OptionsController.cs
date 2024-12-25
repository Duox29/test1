using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test1.Models;

namespace test1.Controllers
{
    public class OptionsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Options
        public ActionResult Index()
        {
            var options = db.Options.Include(o => o.Product);
            return View(options.ToList());
        }

        // GET: Options/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option option = db.Options.Find(id);
            if (option == null)
            {
                return HttpNotFound();
            }
            return View(option);
        }

        // GET: Options/Create
        public ActionResult Create(int? id)
        {
            ViewBag.product_id = id.Value;
            return View();
        }

        // POST: Options/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "option_id,product_id,description,price,stock")] Option option)
        {
            if (ModelState.IsValid)
            {
                db.Options.Add(option);
                db.SaveChanges();
                return RedirectToAction("Option","Admin", new {id = option.product_id});
            }

            ViewBag.product_id = new SelectList(db.Products, "product_id", "name", option.product_id);
            return View(option);
        }

        // GET: Options/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option option = db.Options.Find(id);
            if (option == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.Products, "product_id", "name", option.product_id);
            return View(option);
        }

        // POST: Options/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "option_id,product_id,description,price,stock")] Option option)
        {
            if (ModelState.IsValid)
            {
                db.Entry(option).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Option","Admin", new {id = option.product_id});
            }
            ViewBag.product_id = new SelectList(db.Products, "product_id", "name", option.product_id);
            return View(option);
        }

        // GET: Options/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option option = db.Options.Find(id);
            if (option == null)
            {
                return HttpNotFound();
            }
            return View(option);
        }

        // POST: Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Option option = db.Options.Find(id);
            int product_id = option.product_id;
            db.Options.Remove(option);
            db.SaveChanges();
            return RedirectToAction("Option","Admin", new {id = product_id});
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
