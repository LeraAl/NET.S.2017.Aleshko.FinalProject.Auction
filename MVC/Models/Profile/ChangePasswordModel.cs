using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Profile
{
    public class ChangePasswordModel
    {
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords must match.")]
        public string ConfirmPassword { get; set; }
    }
}