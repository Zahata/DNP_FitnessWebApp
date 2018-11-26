using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VIA_Fitness.Models
{
    [MetadataType(typeof(UserData))]
    public partial class User
    {
        public string confirmPassword { get; set; }
    }
    public class UserData
    {
        [Key]
        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username required")]
        public string username { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage = "Minimum 6 characters required")]
        public string password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please confirm password")]
        [DataType(DataType.Password)]
        [Compare("password",ErrorMessage ="Confirm password must match password")]
        public string confirmPassword { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "E-mail required")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "First name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        public string firstName { get; set; }

        [Display(Name = "Last name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string lastName { get; set; }

        [Display(Name = "Age")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter age")]
        public Nullable<int> age { get; set; }

        [Display(Name = "Gender")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose your gender")]
        public string gender { get; set; }

        [Display(Name = "Country")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a country")]
        public string country { get; set; }
      
    }
}