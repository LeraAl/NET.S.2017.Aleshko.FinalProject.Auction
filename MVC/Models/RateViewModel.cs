using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class RateViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }

        public int LotId { get; set; }

        [Display(Name = "Lot")]
        public string LotName { get; set; }

        [Display(Name = "Rate")]
        public decimal RateSize { get; set; }

        [Display(Name = "Date & Time")]
        public DateTime Datetime { get; set; }
    }
}