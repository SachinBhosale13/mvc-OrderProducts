using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class OrdersModel
    {
        [Required]
        [Display(Name = "Order ID")]
        public int OrderID { get; set; }
        [Required]
        [Display(Name = "Order Date")]
        public string OrderDate { get; set; }
        [Required]
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required]
        [Display(Name = "Total Qty")]
        public int TotalQty { get; set; }
        [Required]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }
    }
}
