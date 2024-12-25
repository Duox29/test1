using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test1.Models;

namespace test1.Controllers
{
    public class ImageController : Controller
    {
        private Model1 db = new Model1();
        // GET: Image
        public ActionResult Image_Management(int? id)
        {
            var imagepr = db.Product_Preview_Images.Where(x => x.product_id == id).FirstOrDefault();
            var image = db.Product_Images.Where( x => x.product_id == id).ToList();
            ViewBag.ImagePreview = imagepr?.image_url;
            ViewBag.Images = image.Select(x => x.image_url).ToList();
            ViewBag.product_id = id.Value;
            return View();
        }

        //Upload

        public ActionResult Upload(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
                ViewBag.product_id = id.Value;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase previewImage, IEnumerable<HttpPostedFileBase> productImages, int product_id)
        {
            // Check if the product already has a preview image
            var existingPreview = db.Product_Preview_Images.FirstOrDefault(p => p.product_id == product_id);

            // Check if the product already has product images
            var existingProductImages = db.Product_Images.Where(p => p.product_id == product_id).ToList();

            // Validation: ensure exactly 1 preview image and at most 5 product images
            if (previewImage == null || previewImage.ContentLength == 0)
            {
                ModelState.AddModelError("", "Please upload exactly one preview image.");
            }

            if (productImages == null || productImages.Count() == 0 || productImages.Count() > 5)
            {
                ModelState.AddModelError("", "Please upload between 1 to 5 product images.");
            }

            if (!ModelState.IsValid)
            {
                // Return to the view with error messages if validation fails
                return View();
            }

            // Overwrite the preview image if it exists, or create a new one
            if (previewImage != null && previewImage.ContentLength > 0)
            {
                string previewImagePath = Path.Combine(Server.MapPath("~/Images"), product_id + "_preview" + Path.GetExtension(previewImage.FileName));

                // If an existing preview image is found, delete the old file
                if (existingPreview != null)
                {
                    string oldPreviewPath = Server.MapPath(existingPreview.image_url);
                    if (System.IO.File.Exists(oldPreviewPath))
                    {
                        System.IO.File.Delete(oldPreviewPath);
                    }
                    // Update the existing entry
                    existingPreview.image_url = "/Images/" + product_id + "_preview" + Path.GetExtension(previewImage.FileName);
                }
                else
                {
                    // Create a new entry for the preview image
                    db.Product_Preview_Images.Add(new Product_Preview_Image
                    {
                        product_id = product_id,
                        image_url = "/Images/" + product_id + "_preview" + Path.GetExtension(previewImage.FileName)
                    });
                }

                // Save the new preview image
                previewImage.SaveAs(previewImagePath);
            }

            // Handle product images (overwrite if necessary)
            if (productImages != null && productImages.Any())
            {
                // Delete existing product images
                foreach (var existingImage in existingProductImages)
                {
                    string oldImagePath = Server.MapPath(existingImage.image_url);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    db.Product_Images.Remove(existingImage);
                }

                // Add new product images
                foreach (var file in productImages.Take(5))
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string filePath = Path.Combine(Server.MapPath("~/Images"), product_id + "_product_" + Path.GetFileName(file.FileName));
                        file.SaveAs(filePath);

                        db.Product_Images.Add(new Product_Images
                        {
                            product_id = product_id,
                            image_url = "/Images/" + product_id + "_product_" + Path.GetFileName(file.FileName)
                        });
                    }
                }
            }

            db.SaveChanges();
            return RedirectToAction("Image_Management", "Image", new { id = product_id });
        }


        //Xóa ảnh


    }
}