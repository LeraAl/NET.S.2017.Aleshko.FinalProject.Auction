using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Profile
{
    public class ProfileForAdminModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        [Display(Name = "Lot Count")]
        public int LotCount { get; set; }

        [Display(Name = "Rate Count")]
        public int RateCount { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}