using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Lot
{
    public class LotModeratorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Owner")]
        public string OwnerName { get; set; }

        [Display(Name = "Owner Email")]
        public string OwnerEmail { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDatetime { get; set; }

        [Display(Name = "Rate Count")]
        public int RateCount { get; set; }

        [Display(Name = "Last Rate Date")]
        [DataType(DataType.Date)]
        public DateTime? LastRateDateTime { get; set; }
    }
}