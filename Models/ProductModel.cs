using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ProductModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Product Image")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string file { get; set; }

        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Required]
        //[RegularExpression("^\\d+(\\.\\d+)*$", ErrorMessage = "Should be simple number or decimal number")]
        [RegularExpression("^[0-9]{0,10}\\.[0-9]{0,2}$", ErrorMessage = "must be decimal number of 10 no. max to left of decimal point and 2 no. max to right")]
        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } 
    }
}
