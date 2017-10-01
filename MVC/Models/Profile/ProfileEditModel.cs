using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Profile
{
    public class ProfileEditModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}