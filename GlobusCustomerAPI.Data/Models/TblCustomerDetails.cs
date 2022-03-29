using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlobusCustomerAPI.Data.Models
{
    public class TblCustomerDetails
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Required(ErrorMessage = "The Phone Number field is required")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "The Email field is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Password field is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [StringLength(100)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "The State field is required")]
        [Display(Name = "State Of Residence")]
        public string StateOfResidence { get; set; }
        [StringLength(100)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "The LGA field is required")]
        [Display(Name = "LGA")]
        public string LGA { get; set; }
    }
}
