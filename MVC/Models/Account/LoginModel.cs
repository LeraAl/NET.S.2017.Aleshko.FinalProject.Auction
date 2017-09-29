using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Account
{
    public class LoginModel
    {
        public string Login { get; set; }

        [DataType("Password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}