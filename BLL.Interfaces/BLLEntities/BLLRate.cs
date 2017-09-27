using System;

namespace BLL.Interfaces.BLLEntities
{
    public class BLLRate
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        //public int UserName { get; set; } ToAsk

        public int LotId { get; set; }
        //public string LotName { get; set; } ToAsk

        public decimal RateSize { get; set; }

        public DateTime Datetime { get; set; }
    }
}