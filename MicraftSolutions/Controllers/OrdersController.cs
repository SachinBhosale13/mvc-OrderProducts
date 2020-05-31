using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLayer;
using System.IO;
using System.Data;
using System.Data.OleDb;

namespace MicraftSolutions.Controllers
{
    public class OrdersController : Controller
    {
        Business objBusiness = new Business();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllOrders()
        {
            List<OrdersModel> lstOrders = new List<OrdersModel>();
            try
            {
                lstOrders = objBusiness.GetAllOrders();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(lstOrders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int orderId)
        {
            List<OrderDetailsModel> lstOrderDetails = new List<OrderDetailsModel>();
            try
            {
                lstOrderDetails = objBusiness.GetOrdersDetailsById(orderId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(lstOrderDetails);
        }

        [HttpGet]
        public ActionResult AddNewOrder()
        {
            List<CustomerModel> lstCust = new List<CustomerModel>();
            List<ProductModel> lstProd = new List<ProductModel>();
            try
            {
                lstCust = objBusiness.GetAllCustomers();

                ViewBag.Customers = lstCust;

                lstProd = objBusiness.GetAllProducts();

                ViewBag.Products = lstProd;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        [HttpPost]
        public bool SubmitNewOrder(NewOrderModel data)
        {
            Boolean result = false;
            try
            {
                int orderId = 0;
                orderId = objBusiness.AddOrderAndDetails(data);

                if (orderId > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public JsonResult GetOrdersDetailsById(int orderId)
        {
            List<OrderDetailsModel> lstOrdersDetails = new List<OrderDetailsModel>();
            try
            {
                lstOrdersDetails = objBusiness.GetOrdersDetailsById(orderId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(lstOrdersDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int orderId)
        {
            List<CustomerModel> lstCust = new List<CustomerModel>();
            List<ProductModel> lstProd = new List<ProductModel>();

            try
            {
                lstCust = objBusiness.GetAllCustomers();

                ViewBag.Customers = lstCust;

                lstProd = objBusiness.GetAllProducts();

                ViewBag.Products = lstProd;

                TempData["orderId"] = orderId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }



        public ActionResult ImportExcel(int custId, int orderId)
        {
            TempData["CustomerId"] = custId;

            TempData["orderId"] = orderId;


            return View();
        }


        [HttpPost]
        public ActionResult Importexcel()
        {
            NewOrderModel objNewOrder = new NewOrderModel();
            int custId = 0;
            int orderId = 0;
            try
            {
                custId = Convert.ToInt32(TempData["CustomerId"]);
                orderId = Convert.ToInt32(TempData["orderId"]);

                objNewOrder.OrderID = orderId;

                if (custId > 0)
                {
                    objNewOrder.CustomerID = custId;

                    DataTable dt = new DataTable();

                    if (Request.Files["FileUpload1"].ContentLength > 0)
                    {
                        string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                        string query = null;
                        string connString = "";

                        string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                        string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                        if (!Directory.Exists(path1))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                        }
                        if (validFileTypes.Contains(extension))
                        {
                            if (System.IO.File.Exists(path1))
                            {
                                System.IO.File.Delete(path1);
                            }
                            Request.Files["FileUpload1"].SaveAs(path1);

                            //Connection String to Excel Workbook  
                            if (extension.Trim() == ".xls")
                            {
                                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                                dt = ConvertXSLXtoDataTable(path1, connString);
                                ViewBag.Data = dt;
                            }
                            else if (extension.Trim() == ".xlsx")
                            {
                                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                                dt = ConvertXSLXtoDataTable(path1, connString);
                                ViewBag.Data = dt;
                            }
                            else if (extension == ".csv")
                            {
                                dt = ConvertCSVtoDataTable(path1);
                                ViewBag.Data = dt;
                            }

                            if (dt != null && dt.Rows.Count > 1)
                            {
                                List<OrderDetailsModel> lstOrderDetails = new List<OrderDetailsModel>();
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    OrderDetailsModel objOrderDetails = new OrderDetailsModel();

                                    objOrderDetails.ProductCode = Convert.ToString(dt.Rows[i][0]);
                                    objOrderDetails.Qty = Convert.ToInt32(dt.Rows[i][1]);

                                    ProductModel objProduct = new ProductModel();

                                    objProduct = objBusiness.GetProductByCode(objOrderDetails.ProductCode);

                                    if (objProduct != null)
                                    {
                                        objOrderDetails.Amount = objProduct.Rate * objOrderDetails.Qty;

                                        objNewOrder.TotalAmount = objNewOrder.TotalAmount + objOrderDetails.Amount;

                                        objNewOrder.TotalQty = objNewOrder.TotalQty + objOrderDetails.Qty;

                                        lstOrderDetails.Add(objOrderDetails);
                                    }

                                }

                                objNewOrder.lstOrderDetails = lstOrderDetails;


                                if (objNewOrder.lstOrderDetails != null)
                                {
                                    orderId = objBusiness.AddOrderAndDetails(objNewOrder);

                                    if (orderId > 0)
                                    {
                                        ViewBag.Result = "Success: Excel Uploaded Successfully!";

                                        // Source file to be renamed  
                                        string sourceFile = path1;
                                        // Create a FileInfo  
                                        System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                                        // Check if file is there  
                                        if (fi.Exists)
                                        {
                                            string fileName = "OrderID_" + orderId + System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();

                                            string newFilePath = Server.MapPath("~/Content/Uploads/") + fileName;

                                            if (System.IO.File.Exists(newFilePath))
                                            {
                                                System.IO.File.Delete(newFilePath);

                                            }

                                            fi.MoveTo(Server.MapPath("~/Content/Uploads/") + fileName);

                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Error = "Failure: Something went wrong while uploading.";
                                    }
                                }


                            }

                        }
                        else
                        {
                            ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                        }
                    }
                }
                else
                {
                    ViewBag.Error = "Something went wrong";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }


        public static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {

                oledbConn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
                {
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);

                    dt = ds.Tables[0];
                }
            }
            catch
            {
            }
            finally
            {
                oledbConn.Close();
            }

            return dt;

        }

        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }

            }


            return dt;
        }


        [HttpGet]
        public ActionResult Create()
        {
            List<CustomerModel> lstCust = new List<CustomerModel>();
            try
            {
                lstCust = objBusiness.GetAllCustomers();

                ViewBag.Customers = lstCust;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(OrdersModel data)
        {
            Boolean result = false;

            try
            {
                //result = objBusiness.AddNewOrder(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int orderId)
        {
            try
            {
                objBusiness.DeleteOrder(orderId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("Index");
        }

    }
}