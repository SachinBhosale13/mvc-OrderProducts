using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLayer;
using System.IO;

namespace MicraftSolutions.Controllers
{
    public class ProductController : Controller
    {
        Business objBusiness = new Business();

        
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllProducts()
        {
            List<ProductModel> lstProduct = new List<ProductModel>();
            try
            {
                lstProduct = objBusiness.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(lstProduct, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file,ProductModel data)
        {
            Boolean result = false;

            try
            {
                if (file != null) 
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Product_Images"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    if (!string.IsNullOrWhiteSpace(path)) 
                    {
                        data.ProductImage = "/Content/Product_Images/" + file.FileName;
                        result = objBusiness.AddNewProduct(data);

                        if (result)
                        {
                            ViewBag.Result = "SUCCESS: " + data.ProductName + " added successfully.";
                        }
                        else
                        {
                            ViewBag.Error = "Failure: Something went wrong.";
                        }
                    }                   
                }
                
            }
            catch (Exception ex)
            {                
                throw ex;
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(string pCode) 
        {
            ProductModel objProduct = new ProductModel();
            try
            {
                objProduct = objBusiness.GetProductByCode(pCode);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, ProductModel data) 
        {
            Boolean result = false;
            try
            {
                if (file != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Product_Images"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        data.ProductImage = "/Content/Product_Images/" + file.FileName;                
                    }
                }

                result = objBusiness.EditProduct(data);

                if (result)
                {
                    ViewBag.Result = "SUCCESS: " + data.ProductName + " updated successfully.";
                }
                else
                {
                    ViewBag.Error = "Failure: Something went wrong.";
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return View();
        }

        public JsonResult GetProductByCode(string pCode)
        {
            ProductModel objProduct = new ProductModel();
            try
            {
                objProduct = objBusiness.GetProductByCode(pCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(objProduct, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string pCode) 
        {
            try
            {
                objBusiness.DeleteProduct(pCode);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return RedirectToAction("Index");
        }

    }
}