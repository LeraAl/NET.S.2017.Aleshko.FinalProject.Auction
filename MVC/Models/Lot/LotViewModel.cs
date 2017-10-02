using System;

namespace MVC.Models.Lot
{
    public class LotViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public decimal StartPrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public string State { get; set; }

        public string OwnerName { get; set; }

        public string OwnerEmail { get; set; }

        public string Category { get; set; }

        public DateTime StartDatetime { get; set; }

        public string FinalBuyer { get; set; }
    }
}