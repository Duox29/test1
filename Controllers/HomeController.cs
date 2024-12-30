using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using test1.Models;

namespace test1.Controllers
{
    public class HomeController : Controller
    {
        private Model1 db = new Model1();
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Product_Preview_Images).Include(p => p.Options).Include(p => p.Sale).ToList();
            return View(products.ToList());
        }

        //Search
        public ActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return View();
            }

            // Tìm kiếm sản phẩm bằng Full-Text Search
            var products = db.Products
                             .Where(p => SqlFunctions.PatIndex("%" + query + "%", p.brand) > 0
                                        || SqlFunctions.PatIndex("%" + query + "%", p.name) > 0
                                        || SqlFunctions.PatIndex("%" + query + "%", p.model) > 0)
                             .ToList();

            return View(products);
        }


        //Login

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginInfo model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.email == model.Email && u.password == model.Password);
                if (user != null)
                {
                    Session["UserID"] = user.user_id;
                    Session["Username"] = user.username;
                    Session["Role"] = user.role_level;
                    user.last_login = DateTime.Now;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác.");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        //Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "user_id,username,email,password,first_name,last_name,phone_number,address_line1,registration_date,last_login,is_active,role_level")] User user)
        {
            if (ModelState.IsValid)
            {
                user.registration_date = DateTime.Now;
                user.role_level = "customer";
                user.is_active = true;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login","Home");
            }

            return View(user);
        }


        //Profile
        public ActionResult Profile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User profile = db.Users.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //Edit account information
        public ActionResult eUser(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult eUser([Bind(Include = "user_id,username,email,password,first_name,last_name,phone_number,address_line1,registration_date,last_login,is_active,role_level")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile","Home",new {id = user.user_id});
            }
            return View(user);
        }
        //View Item Info
        public ActionResult ItemDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products
                .Include(x => x.Product_Preview_Images)
                .Include(x => x.Product_Images)
                .Include(x => x.Product_Specifications)
                .Include(x => x.Options)
                .FirstOrDefault(x => x.product_id == id);
            return View(product);
        }

        //Add to cart 
        [HttpPost]
        public ActionResult AddToCart(int product_id, int option_id)
        {
            if (Session["UserID"] == null)
            {
                return Json(new { success = false, message = "Quý khách chưa đăng nhập, vui lòng đăng nhập để sử dụng chức năng." });
            }
            int userId = (int)Session["UserID"];
            var cart = db.Carts.FirstOrDefault(x => x.user_id == userId);

            if (cart == null)
            {
                cart = new Cart();
                cart.user_id = userId;
                cart.created_at = DateTime.Now;

                db.Carts.Add(cart);
                db.SaveChanges();
            }


            var citem = db.Cart_Items.FirstOrDefault(x => x.cart_id == cart.cart_id && x.product_id == product_id && x.option_id == option_id);

            if (citem != null)
            {
                citem.quantity += 1;
                citem.added_at = DateTime.Now;
            }
            else
            {
                citem = new Cart_Items();
                citem.cart_id = cart.cart_id;
                citem.product_id = product_id;
                citem.option_id = option_id;
                citem.quantity = 1;
                citem.added_at = DateTime.Now;

                db.Cart_Items.Add(citem);
            }

            db.SaveChanges();

            return Json(new { success = true, message = "Product added to cart." });
        }



        //Giao diện cart

        public ActionResult Cart(int? userID)
        {
            if (userID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "User ID is missing" + userID);
            }

            // Kiểm tra xem userID có tồn tại trong cơ sở dữ liệu không
            var cart = db.Carts.FirstOrDefault(x => x.user_id == userID);
            if (cart == null)
            {
                return HttpNotFound("No cart found for this user.");
            }

            var cart_items = db.Cart_Items.Where(x => x.cart_id == cart.cart_id).ToList();

            if (cart_items == null || !cart_items.Any())
            {
                // Nếu giỏ hàng không có sản phẩm, bạn có thể thông báo cho người dùng
                ViewBag.Message = "Your cart is empty.";
            }

            return View(cart_items);
        }

        //Update quantity in Cart View
        [HttpPost]
        public ActionResult UpdateCartItem(int cartItemId, int newQuantity)
        {
            try
            {
                // Tìm item trong giỏ hàng dựa vào cartItemId
                var cartItem = db.Cart_Items.FirstOrDefault(x => x.cart_item_id == cartItemId);

                if (cartItem == null)
                {
                    return Json(new { success = false, message = "Mục giỏ hàng không tồn tại." });
                }

                // Cập nhật số lượng mới
                cartItem.quantity = newQuantity;
                db.SaveChanges();

                return Json(new { success = true, message = "Cập nhật số lượng thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        //Xóa sp khỏi giỏ hàng
        [HttpPost]
        public JsonResult RemoveCartItem(int? cartItemId)
        {
            var cartItem = db.Cart_Items.FirstOrDefault(x => x.cart_item_id == cartItemId);
            if (cartItem != null)
            {
                db.Cart_Items.Remove(cartItem);
                db.SaveChanges();
                return Json(new { success = true, message ="Thành công" });
            }
            return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng." });
        }


        public ActionResult ConfirmCart(int? id)
        {
            var cartItem = db.Cart_Items.Where(x => x.cart_id == id).ToList();
            ViewBag.total = cartItem.Sum(x => x.Option.price * x.quantity);
            ViewBag.cart_id = id;
            var user = db.Carts.Where(x => x.cart_id == id).Select(c => c.User).FirstOrDefault();
            ViewBag.CustomerName = user.first_name.ToString()+" " + user.last_name.ToString();
            ViewBag.PhoneNumber = user.phone_number.ToString();
            ViewBag.Address = user.address_line1.ToString();
            return View(cartItem.ToList());
        }
        [HttpPost]
        public ActionResult ConfirmOrder(string Address, string PaymentMethod, int cart_id)
        {
            Order order = new Order();
            order.user_id = (int)Session["UserID"];
            order.order_date = DateTime.Now;
            order.status = "Pending";
            order.shipping_address = Address.ToString();
            order.payment_method = PaymentMethod.ToString();
            db.Orders.Add(order);
            db.SaveChanges();
            //Add cart item to order item
            int order_id = order.order_id;
            var cartItem = db.Cart_Items.Where(x => x.cart_id == cart_id).ToList();
            foreach(var citem in cartItem)
            {
                Order_Item oitem = new Order_Item
                {
                    order_id = order_id,
                    product_id = (int)citem.product_id,
                    option_id = (int)citem.option_id,
                    quantity = (int)citem.quantity
                };
                db.Order_Items.Add(oitem);
            }
            db.SaveChanges();

            return RedirectToAction("Order","Home", new { id = (int)Session["UserID"] });
        }

        public ActionResult Order(int? id)
        {
            var list_order = db.Orders.Where(x => x.user_id == id).ToList();
            return View(list_order);
        }
    }
}   