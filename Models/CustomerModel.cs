using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class CustomerModel
    {
        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = " only alphabets and spaces are allowed")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = " only alphabets and spaces are allowed")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Pincode")]
        [RegularExpression("^0*[1-9][0-9]{5}",ErrorMessage="PinCode must be 6 digit number without starting with 0")]
        
        public string pincode { get; set; }
    }
}
