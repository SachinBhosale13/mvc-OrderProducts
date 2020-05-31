using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Models;

namespace MicraftSolutions.Controllers
{
    public class CustomerController : Controller
    {
        Business objBusiness = new Business();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllCustomers()
        {
            List<CustomerModel> lstCust = new List<CustomerModel>();
            try
            {
                lstCust = objBusiness.GetAllCustomers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(lstCust, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomerModel data)
        {
            Boolean result = false;

            try
            {               
                result = objBusiness.AddNewCustomer(data);

                if (result)
                {
                    ViewBag.Result = "SUCCESS: " + data.Name + " added successfully.";
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CustomerModel objCust = new CustomerModel();
            try
            {
                objCust = objBusiness.GetCustomerById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objCust);
        }

        [HttpPost]
        public ActionResult Edit(CustomerModel data)
        {
            Boolean result = false;
            try
            {
                result = objBusiness.EditCustomer(data);

                if (result)
                {
                    ViewBag.Result = "SUCCESS: " + data.Name + " updated successfully.";
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


        public ActionResult Delete(int custId)
        {
            try
            {
                objBusiness.DeleteCustomer(custId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("Index");
        }

	}
}