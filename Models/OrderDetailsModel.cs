using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class OrderDetailsModel
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Order ID")]
        public int OrderID { get; set; }
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }
        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Rate")]
        public decimal Rate { get; set; }
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Quantity")]
        public int Qty {get;set;}
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
        [Display(Name = "Total Quantity")]
        public int TotalQty { get; set; }
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }
    }
}
