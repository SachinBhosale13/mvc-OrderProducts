using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataAccessLayer;

namespace BusinessLayer
{
    public class Business
    {
        DataAccess objData = new DataAccess();

        public List<ProductModel> GetAllProducts()
        {
            try
            {
                return objData.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddNewProduct(ProductModel data)
        {
            Boolean result = false;

            try
            {
                result = objData.AddNewProduct(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public ProductModel GetProductByCode(string pCode)
        {
            try
            {
                return objData.GetProductByCode(pCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditProduct(ProductModel data)
        {
            try
            {
                return objData.EditProduct(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteProduct(string pCode) 
        {
            try
            {
                return objData.DeleteProduct(pCode);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }


        //Customer
        public List<CustomerModel> GetAllCustomers()
        {
            try
            {
                return objData.GetAllCustomers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddNewCustomer(CustomerModel data)
        {
            Boolean result = false;

            try
            {
                result = objData.AddNewCustomer(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public CustomerModel GetCustomerById(int id)
        {
            try
            {
                return objData.GetCustomerById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditCustomer(CustomerModel data)
        {
            try
            {
                return objData.EditCustomer(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteCustomer(int custId)
        {
            try
            {
                return objData.DeleteCustomer(custId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Orders
        public List<OrdersModel> GetAllOrders()
        {
            try
            {
                return objData.GetAllOrders();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public int AddOrderAndDetails(NewOrderModel data)
        {
            int orderId = 0;

            try
            {
                orderId = objData.AddOrderAndDetails(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orderId;
        }


        public List<OrderDetailsModel> GetOrdersDetailsById(int id) 
        {
            try
            {
                return objData.GetOrdersDetailsById(id);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }


        public bool DeleteOrder(int orderId)
        {
            try
            {
                return objData.DeleteOrder(orderId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public bool AddNewOrder(OrdersModel data)
        //{
        //    Boolean result = false;

        //    try
        //    {
        //        result = objData.AddNewOrder(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}
    }
}
