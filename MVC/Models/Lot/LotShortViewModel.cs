using System;

namespace MVC.Models.Lot
{
    public class LotShortViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }
        
        public decimal CurrentPrice { get; set; }

        public string OwnerName { get; set; }

        public DateTime StartDatetime { get; set; }
    }
}