using System;

namespace DAL.Interfaces.DTO
{
    public class DALRate: IEntity
    {
        public int Id { get; set; }

        public int LotId { get; set; }

        public int UserId { get; set; }
        
        public DateTime Datetime { get; set; }
        
        public decimal RateSize { get; set; }
    }
}