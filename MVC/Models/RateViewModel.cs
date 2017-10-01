using System;

namespace MVC.Models
{
    public class RateViewModel
    {
        public string UserName { get; set; }
        
        public string LotName { get; set; }

        public decimal RateSize { get; set; }
        
        public DateTime Datetime { get; set; }
    }
}