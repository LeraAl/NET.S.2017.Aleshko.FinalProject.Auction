using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Account
{
    public class RegisterModel
    {
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType("Password")]
        public string Password { get; set; }

        [DataType("Password")]
        [Compare("Password", ErrorMessage = "Passwords must mathch")]
        public string ConfirmPassword { get; set; }

        public bool RememberMe { get; set; }
    }
}