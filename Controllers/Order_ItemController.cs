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
    public class Order_ItemController : Controller
    {
        private Model1 db = new Model1();

        // GET: Order_Item
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.order_id = id.Value;
            var order_Items = db.Order_Items.Where(o => o.order_id == id).Include(o => o.Option).Include(o => o.Order).Include(o => o.Product);
            return View(order_Items.ToList());
        }

        // GET: Order_Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Item order_Item = db.Order_Items.Find(id);
            if (order_Item == null)
            {
                return HttpNotFound();
            }
            return View(order_Item);
        }

        // GET: Order_Item/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.option_id = new SelectList(db.Options, "option_id", "description");
            ViewBag.order_id = (int)id.Value;
            return View();
        }

        // POST: Order_Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_item_id,order_id,product_id,option_id,quantity")] Order_Item order_Item)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem đã có order_item với product_id, option_id, và order_id chưa
                var existingItem = db.Order_Items
                      .FirstOrDefault(oi => oi.order_id == order_Item.order_id
                                         && oi.product_id == order_Item.product_id
                                         && oi.option_id == order_Item.option_id);


                if (existingItem != null)
                {
                    // Nếu tồn tại, cập nhật quantity
                    existingItem.quantity += order_Item.quantity;
                    db.Entry(existingItem).State = EntityState.Modified;
                }
                else
                {
                    // Nếu không tồn tại, thêm bản ghi mới
                    db.Order_Items.Add(order_Item);
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Order_Item", new { id = order_Item.order_id });
            }

            ViewBag.option_id = new SelectList(db.Options, "option_id", "description", order_Item.option_id);
            ViewBag.order_id = new SelectList(db.Orders, "order_id", "status", order_Item.order_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "name", order_Item.product_id);
            return View(order_Item);
        }


        // GET: Order_Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Item order_Item = db.Order_Items.Find(id);
            if (order_Item == null)
            {
                return HttpNotFound();
            }
            ViewBag.option_id = new SelectList(db.Options, "option_id", "description", order_Item.option_id);
            ViewBag.order_id = new SelectList(db.Orders, "order_id", "status", order_Item.order_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "name", order_Item.product_id);
            return View(order_Item);
        }

        // POST: Order_Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_item_id,order_id,product_id,option_id,quantity")] Order_Item order_Item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Order_Item", new {id = order_Item.order_id});
            }
            ViewBag.product_id = new SelectList(db.Products, "product_id", "name", order_Item.product_id);
            return View(order_Item);
        }

        // GET: Order_Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Item order_Item = db.Order_Items.Find(id);
            if (order_Item == null)
            {
                return HttpNotFound();
            }
            return View(order_Item);
        }

        // POST: Order_Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Item order_Item = db.Order_Items.Find(id);
            int order_id = order_Item.order_id;
            db.Order_Items.Remove(order_Item);
            db.SaveChanges();
            return RedirectToAction("Index","Order_Item",new {id = order_id});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Update option
        public JsonResult GetOptionsByProductId(int productId)
        {
            var options = db.Options.Where(o => o.product_id == productId)
                                    .Select(o => new { o.option_id, o.description }).ToList();

            return Json(options, JsonRequestBehavior.AllowGet);
        }


    }
}
