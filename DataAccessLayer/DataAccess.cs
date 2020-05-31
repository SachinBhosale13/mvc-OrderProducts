using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DataAccess
    {
        string connString = ConfigurationManager.ConnectionStrings["MicraftConn"].ToString();


        #region product
        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> lstProduct = new List<ProductModel>();

            SqlConnection conn = new SqlConnection(connString);

            try
            {
                SqlCommand cmd = new SqlCommand("GetAllProducts", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ProductModel objProduct = new ProductModel();

                            objProduct.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                            objProduct.ProductCode = Convert.ToString(dt.Rows[i]["ProductCode"]);
                            objProduct.ProductName = Convert.ToString(dt.Rows[i]["ProductName"]);
                            objProduct.ProductImage = Convert.ToString(dt.Rows[i]["ProductImage"]);
                            objProduct.Unit = Convert.ToString(dt.Rows[i]["Unit"]);
                            objProduct.Rate = Convert.ToDecimal(dt.Rows[i]["Rate"]);
                            objProduct.Description = Convert.ToString(dt.Rows[i]["Description"]);

                            lstProduct.Add(objProduct);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return lstProduct;
        }

        public bool AddNewProduct(ProductModel data) 
        {
            Boolean result = false;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlCommand cmd = new SqlCommand("AddNewProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductCode", data.ProductCode);
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@ProductImage", data.ProductImage);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Rate", data.Rate);
                cmd.Parameters.AddWithValue("@Description", data.Description);

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                int res = 0;
                res = cmd.ExecuteNonQuery();

                if (res > 0) 
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return result;
        }

        public ProductModel GetProductByCode(string pCode) 
        {
            SqlConnection conn = new SqlConnection(connString);
            ProductModel objProduct = new ProductModel();

            try
            {
                SqlCommand cmd = new SqlCommand("GetProductByCode", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductCode", pCode);

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read()) 
                    {
                        objProduct.ID = Convert.ToInt32(rdr["ID"]);
                        objProduct.ProductCode = Convert.ToString(rdr["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(rdr["ProductName"]);
                        objProduct.ProductImage = Convert.ToString(rdr["ProductImage"]);
                        objProduct.Unit = Convert.ToString(rdr["unit"]);
                        objProduct.Rate = Convert.ToDecimal(rdr["Rate"]);
                        objProduct.Description = Convert.ToString(rdr["Description"]);
                    }
                }
            }
            catch (Exception ex) 
            {                
                throw ex;
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return objProduct;
        }

        public bool EditProduct(ProductModel data)
        {
            Boolean result = false;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlCommand cmd = new SqlCommand("EditProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", data.ID);
                cmd.Parameters.AddWithValue("@ProductCode", data.ProductCode);
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@ProductImage", data.ProductImage);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Rate", data.Rate);
                cmd.Parameters.AddWithValue("@Description", data.Description);

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                int res = 0;
                res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return result;
        }

        public bool DeleteProduct(string pCode) 
        {
            Boolean result = false;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlCommand cmd = new SqlCommand("deleteProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductCode", pCode);

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                int res = 0;
                res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return result;
        }

        #endregion



        //Customer
        #region customer
        public List<CustomerModel> GetAllCustomers()
        {
            List<CustomerModel> lstCust = new List<CustomerModel>();

            SqlConnection conn = new SqlConnection(connString);

            try
            {
                SqlCommand cmd = new SqlCommand("GetAllCustomers", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CustomerModel objCust = new CustomerModel();

                            objCust.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                            objCust.Name = Convert.ToString(dt.Rows[i]["Name"]);
                            objCust.Address = Convert.ToString(dt.Rows[i]["Address"]);
                            objCust.City = Convert.ToString(dt.Rows[i]["City"]);
                            objCust.pincode = Convert.ToString(dt.Rows[i]["pincode"]);
                            
                            lstCust.Add(objCust);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return lstCust;
        }

        public bool AddNewCustomer(CustomerModel data)
        {
            Boolean result = false;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlCommand cmd = new SqlCommand("AddNewCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", data.Name);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@pincode", data.pincode);
               

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                int res = 0;
                res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return result;
        }

        public CustomerModel GetCustomerById(int id)
        {
            SqlConnection conn = new SqlConnection(connString);
            CustomerModel objCust = new CustomerModel();

            try
            {
                SqlCommand cmd = new SqlCommand("GetCustomerById", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        objCust.ID = Convert.ToInt32(rdr["ID"]);
                        objCust.Name = Convert.ToString(rdr["Name"]);
                        objCust.Address = Convert.ToString(rdr["Address"]);
                        objCust.City = Convert.ToString(rdr["City"]);
                        objCust.pincode = Convert.ToString(rdr["pincode"]);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return objCust;
        }


        public bool EditCustomer(CustomerModel data)
        {
            Boolean result = false;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlCommand cmd = new SqlCommand("EditCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", data.ID);
                cmd.Parameters.AddWithValue("@Name", data.Name);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@pincode", data.pincode);
                

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                int res = 0;
                res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return result;
        }

        public bool DeleteCustomer(int custId)
        {
            Boolean result = false;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlCommand cmd = new SqlCommand("deleteCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@custID", custId);

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                int res = 0;
                res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return result;
        }
        #endregion

        //Orders
        public List<OrdersModel> GetAllOrders()
        {
            List<OrdersModel> lstOrder = new List<OrdersModel>();

            SqlConnection conn = new SqlConnection(connString);

            try
            {
                SqlCommand cmd = new SqlCommand("GetAllOrders", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            OrdersModel objOrder = new OrdersModel();

                            objOrder.OrderID = Convert.ToInt32(dt.Rows[i]["OrderID"]);
                            objOrder.OrderDate = Convert.ToString(dt.Rows[i]["OrderDate"]);
                            objOrder.CustomerID = Convert.ToInt32(dt.Rows[i]["CustomerID"]);
                            objOrder.CustomerName = Convert.ToString(dt.Rows[i]["CustomerName"]);
                            objOrder.TotalQty = Convert.ToInt32(dt.Rows[i]["TotalQty"]);
                            objOrder.TotalAmount = Convert.ToDecimal(dt.Rows[i]["TotalAmount"]);

                            lstOrder.Add(objOrder);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return lstOrder;
        }

        public int AddNewOrder(OrdersModel data)
        {
            int orderId = 0;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlCommand cmd = new SqlCommand("AddNewOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderId", data.OrderID);
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@TotalQty", data.TotalQty);
                cmd.Parameters.AddWithValue("@TotalAmount", data.TotalAmount);

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                
                orderId = Convert.ToInt32(cmd.ExecuteScalar());
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return orderId;
        }


        //new
        public int AddOrderAndDetails(NewOrderModel data) 
        {
            bool result = false;
            int orderId;
            OrdersModel objOrder = new OrdersModel();
            try
            {
                objOrder.OrderID = data.OrderID;
                objOrder.CustomerID = data.CustomerID;
                objOrder.TotalQty = data.TotalQty;
                objOrder.TotalAmount = data.TotalAmount;

                orderId = AddNewOrder(objOrder);

                if (orderId > 0) 
                {
                    DataTable dtOrders = new DataTable();

                    dtOrders = GetOrderDetailsTable(data.lstOrderDetails,orderId);

                    if (dtOrders != null && dtOrders.Rows.Count == data.lstOrderDetails.Count) 
                    {
                        result = AddAllOrderDetails(dtOrders, orderId);
                    }

                    //for (int i = 0; i < data.lstOrderDetails.Count; i++)
                    //{
                    //    data.lstOrderDetails[i].OrderID = orderId;
                    //    result = AddOrderDetails(data.lstOrderDetails[i]);
                    //}
                }
                
            }
            catch (Exception ex)
            {                
                throw ex;
            }

            return orderId;
        }

        public bool AddAllOrderDetails(DataTable dtOrders, int orderId)
        {
            Boolean result = false;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlCommand cmd = new SqlCommand("AddOrderDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderID", orderId);

                cmd.Parameters.AddWithValue("@OrderDetailsType", dtOrders);

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                int res = 0;
                res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return result;
        }

        public bool DeleteOrder(int orderId)
        {
            Boolean result = false;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlCommand cmd = new SqlCommand("deleteOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderID", orderId);

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                int res = 0;
                res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return result;
        }

        

        

        //OrderDetails
        public List<OrderDetailsModel> GetOrdersDetailsById(int id)
        {
            List<OrderDetailsModel> lstOrderDetails = new List<OrderDetailsModel>();

            SqlConnection conn = new SqlConnection(connString);

            try
            {
                SqlCommand cmd = new SqlCommand("GetOrdersDetailsById", conn);
                cmd.Parameters.AddWithValue("@OrderID", id);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            OrderDetailsModel objOrderDetails = new OrderDetailsModel();


                            objOrderDetails.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                            objOrderDetails.OrderID = Convert.ToInt32(dt.Rows[i]["OrderID"]);
                            objOrderDetails.ProductCode = Convert.ToString(dt.Rows[i]["ProductCode"]);
                            objOrderDetails.ProductName = Convert.ToString(dt.Rows[i]["ProductName"]);
                            objOrderDetails.ProductImage = Convert.ToString(dt.Rows[i]["ProductImage"]);
                            objOrderDetails.Unit = Convert.ToString(dt.Rows[i]["Unit"]);
                            objOrderDetails.Rate = Convert.ToDecimal(dt.Rows[i]["Rate"]);
                            objOrderDetails.CustomerID = Convert.ToInt32(dt.Rows[i]["CustomerID"]);
                            objOrderDetails.CustomerName = Convert.ToString(dt.Rows[i]["CustomerName"]);
                            objOrderDetails.Qty = Convert.ToInt32(dt.Rows[i]["Qty"]);
                            objOrderDetails.Amount = Convert.ToDecimal(dt.Rows[i]["Amount"]);

                            objOrderDetails.TotalQty = Convert.ToInt32(dt.Rows[i]["TotalQty"]);
                            objOrderDetails.TotalAmount = Convert.ToDecimal(dt.Rows[i]["TotalAmount"]);

                            lstOrderDetails.Add(objOrderDetails);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return lstOrderDetails;
        }


        public DataTable GetOrderDetailsTable(List<OrderDetailsModel> lstOrderDetails,int OrderId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("OrderID");
                dt.Columns.Add("ProductCode");
                dt.Columns.Add("Qty");
                dt.Columns.Add("Amount");

                foreach (var item in lstOrderDetails)
                {
                    dt.Rows.Add(OrderId, item.ProductCode, item.Qty, item.Amount);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public bool AddOrderDetails(OrderDetailsModel data)
        {
            Boolean result = false;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlCommand cmd = new SqlCommand("AddOrderDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderID", data.OrderID);
                cmd.Parameters.AddWithValue("@ProductCode", data.ProductCode);
                cmd.Parameters.AddWithValue("@Qty", data.Qty);
                cmd.Parameters.AddWithValue("@Amount", data.Amount);

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                int res = 0;
                res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return result;
        }

    }
}
