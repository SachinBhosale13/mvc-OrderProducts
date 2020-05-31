using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NewOrderModel
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }

        public List<OrderDetailsModel> lstOrderDetails { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
