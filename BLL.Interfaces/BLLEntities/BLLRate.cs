using System;

namespace BLL.Interfaces.BLLEntities
{
    public class BLLRate
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int UserName { get; set; }

        public int LotId { get; set; }
        public string LotName { get; set; }

        public decimal RateSixe { get; set; }

        public DateTime DateTime { get; set; }
    }
}