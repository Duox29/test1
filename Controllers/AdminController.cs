using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using test1.Models;

namespace test1.Controllers
{
    public class AdminController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Details/5
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name");
            ViewBag.sale_id = new SelectList(db.Sales, "sale_id", "description");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,category_id,name,brand,model,description,is_active,sale_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Products","Admin");
            }

            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", product.category_id);
            ViewBag.sale_id = new SelectList(db.Sales, "sale_id", "description", product.sale_id);
            return View(product);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", product.category_id);
            ViewBag.sale_id = new SelectList(db.Sales, "sale_id", "description", product.sale_id);
            return View(product);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,category_id,name,brand,model,description,is_active,sale_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Products", "Admin");
            }
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", product.category_id);
            ViewBag.sale_id = new SelectList(db.Sales, "sale_id", "description", product.sale_id);
            return View(product);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);

            if (product != null)
            {
                var orderItems = db.Order_Items.Where(o => o.product_id == id).ToList();
                db.Order_Items.RemoveRange(orderItems);
                var cartItems = db.Cart_Items.Where(c => c.product_id == id).ToList();
                db.Cart_Items.RemoveRange(cartItems);
                var productSpecifications = db.Product_Specifications.Where(p => p.product_id == id).ToList();
                db.Product_Specifications.RemoveRange(productSpecifications);
                var productImages = db.Product_Images.Where(p => p.product_id == id).ToList();
                db.Product_Images.RemoveRange(productImages);
                var productPreviewImages = db.Product_Preview_Images.Where(p => p.product_id == id).ToList();
                db.Product_Preview_Images.RemoveRange(productPreviewImages);
                var options = db.Options.Where(o => o.product_id == id).ToList();
                db.Options.RemoveRange(options);
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("Products","Admin");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //public ActionResult Products()
        //{
        //    var products = db.Products.Include(p => p.Category).Include(p => p.Sale);
        //    return View(products.ToList());
        //}

        //Search

        public ActionResult Products(string searchType = "", string searchTerm = "")
        {
            // Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(searchType) || string.IsNullOrWhiteSpace(searchTerm))
            {
                return View(db.Products.ToList());
            }

            var products = db.Products.AsQueryable();

            // Chuẩn hóa từ khóa tìm kiếm
            searchTerm = searchTerm.Trim().ToLower();

            switch (searchType)
            {
                case "ID":
                    if (int.TryParse(searchTerm, out int productId))
                    {
                        products = products.Where(u => u.product_id == productId);
                    }
                    else
                    {
                        // Xử lý khi ID không hợp lệ
                        ModelState.AddModelError("searchTerm", "Invalid Product ID");
                        return View(new List<Product>());
                    }
                    break;

                case "Category":
                    products = products.Where(u =>
                        u.Category != null &&
                        u.Category.category_name.ToLower().Contains(searchTerm)
                    );
                    break;

                case "Brand":
                    products = products.Where(u =>
                        !string.IsNullOrEmpty(u.brand) &&
                        u.brand.ToLower().Contains(searchTerm)
                    );
                    break;

                default:
                    ModelState.AddModelError("searchType", "Invalid search type");
                    return View(new List<Product>());
            }

            var result = products.ToList();

            // Kiểm tra nếu không tìm thấy kết quả
            if (!result.Any())
            {
                ViewBag.SearchMessage = "Không tìm thấy sản phẩm phù hợp";
            }

            return View(result);
        }


        //View product spe by ID
        public ActionResult Product_Specifications(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound("Product not found"+id);
            }

            Product_Specification product_specification = db.Product_Specifications.SingleOrDefault(x => x.product_id == id);
            if (product_specification == null)
            {
                // Create a new Product_Specification record if it doesn't exist
                product_specification = new Product_Specification
                {
                    product_id = id.Value, // Make sure to pass the correct product_id
                    os = "",
                    cpu = "",
                    gpu = "",
                    ram = "",
                    rom = "",
                    camera = "",
                    screen = "",
                    battery = "",
                    charger = ""
                };

                db.Product_Specifications.Add(product_specification);
                db.SaveChanges(); // Save the new Product_Specification
            }

            return View(product_specification);
        }


        //Edit product spe by ID
        public ActionResult PSEdit(int? id)
        {
            var specification = db.Product_Specifications.SingleOrDefault(x => x.product_id == id);

            if (specification == null)
            {
                specification = new Product_Specification
                {
                    product_id = id.Value, // Make sure to pass the correct product_id
                    os = "",
                    cpu = "",
                    gpu = "",
                    ram = "",
                    rom = "",
                    camera = "",
                    screen = "",
                    battery = "",
                    charger = ""
                };
                db.Product_Specifications.Add(specification);
                db.SaveChanges();
            }

            // Trả về view với model là Product_Specification
            return View(specification);
        }


        [HttpPost]
        public ActionResult PSEdit(Product_Specification specification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specification).State = EntityState.Modified;
                    db.SaveChanges();
                return RedirectToAction("Edit","Admin", new {id = specification.product_id});
            }
            return View(specification);
        }
        public ActionResult Option(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound("Product not found" + id);
            }
            ViewBag.product_id = id;
            var option = db.Options.Where(x => x.product_id == id);
            return View(option.ToList());
        }

        //Dashboard
        public ActionResult Dashboard()
        {
            var totalProduct = db.Products.Count();
            ViewBag.totalProduct = totalProduct;
            DateTime today = DateTime.Now.Date;
            var totalOrdersToday = db.Orders.Where(x => DbFunctions.TruncateTime(x.order_date) == today).ToList();
            ViewBag.totalOrdersToday = totalOrdersToday;
            var totalUsers = db.Users.Count();
            ViewBag.totalUsers = totalUsers;
            var pendingOrders = db.Orders.Where(x => x.status.ToLower().Contains("pending")).Count();
            ViewBag.pendingOrders = pendingOrders;

            return View();
        }

        //Order

        public ActionResult Orders(string searchType = "", string searchTerm = "")
        {
            var orders = db.Orders.AsQueryable(); // Assuming db is your DbContext

            // Perform search based on selected type
            if (!string.IsNullOrEmpty(searchTerm))
            {
                switch (searchType)
                {
                    case "ID":
                        // Try parsing the ID, otherwise return empty result
                        if (int.TryParse(searchTerm, out int orderId))
                        {
                            orders = orders.Where(o => o.order_id == orderId);
                        }
                        break;
                    case "CustomerID":
                        if (int.TryParse(searchTerm, out int customerId))
                        {
                            orders = orders.Where(o => o.user_id == customerId);
                        }
                        break;
                    case "OrderDate":
                        // Use alternative method for date filtering in LINQ to Entities
                        if (DateTime.TryParse(searchTerm, out DateTime orderDate))
                        {
                            orders = orders.Where(o =>
                                o.order_date.HasValue &&
                                o.order_date.Value.Year == orderDate.Year &&
                                o.order_date.Value.Month == orderDate.Month &&
                                o.order_date.Value.Day == orderDate.Day);
                        }
                        break;
                    case "Status":
                        orders = orders.Where(o => o.status.Contains(searchTerm));
                        break;
                }
            }

            return View(orders.ToList());
        }
        public ActionResult Revenue()
        {
            return View();
        }
    }
}
