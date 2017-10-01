using System.Collections.Generic;
using MVC.Models.Lot;

namespace MVC.Models.Profile
{
    public class ProfileViewModel
    {
        public string Login { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IEnumerable<LotShortViewModel> Lots { get; set; }
    }
}