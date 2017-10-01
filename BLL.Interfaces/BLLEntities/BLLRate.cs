using System;

namespace BLL.Interfaces.BLLEntities
{
    public class BLLRate
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; } 

        public int LotId { get; set; }
        public string LotName { get; set; } 

        public decimal RateSize { get; set; }

        public DateTime Datetime { get; set; }
    }
}