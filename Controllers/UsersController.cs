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
    public class UsersController : Controller
    {
        private Model1 db = new Model1();


        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,username,email,password,first_name,last_name,phone_number,address_line1,registration_date,last_login,is_active,role_level")] User user)
        {
            if (ModelState.IsValid)
            {
                user.registration_date = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,username,email,password,first_name,last_name,phone_number,address_line1,registration_date,last_login,is_active,role_level")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            var orders = db.Orders.Where(o => o.user_id == id);
            db.Orders.RemoveRange(orders);

            var cart = db.Carts.Where(c => c.user_id == id);
            db.Carts.RemoveRange(cart);
            db.Users.Remove(user);
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


        public ActionResult Index(string searchType = "", string searchTerm = "")
        {
            var users = db.Users.AsQueryable(); // Assuming db is your DbContext

            // Perform search based on selected type
            if (!string.IsNullOrEmpty(searchTerm))
            {
                switch (searchType)
                {
                    case "ID":
                        // Try parsing the ID, otherwise return empty result
                        if (int.TryParse(searchTerm, out int userId))
                        {
                            users = users.Where(u => u.user_id == userId);
                        }
                        break;
                    case "Username":
                        users = users.Where(u => u.username.Contains(searchTerm));
                        break;
                    case "Email":
                        users = users.Where(u => u.email.Contains(searchTerm));
                        break;
                }
            }

            return View(users.ToList());
        }

    }
}
