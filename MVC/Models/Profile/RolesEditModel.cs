using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVC.Models.Profile
{
    public class RolesEditModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Administator")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Moderator")]
        public bool IsModerator { get; set; }

        [Display(Name = "Banned")]
        public bool IsBanned { get; set; }
    }
}