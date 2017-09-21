using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    public class Rate
    {
        public int Id { get; set; }

        public int LotId { get; set; }

        public int UserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Datetime { get; set; }

        [Column("Rate")]
        public decimal RateSize { get; set; }

        public virtual Lot Lots { get; set; }

        public virtual User Users { get; set; }
    }
}
