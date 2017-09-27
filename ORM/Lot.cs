using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ORM
{
    public class Lot
    {
        [SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lot()
        {
            Rates = new HashSet<Rate>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public int? StateId { get; set; }

        public byte[] Image { get; set; }

        public byte[] Description { get; set; }

        public int OwnerId { get; set; }

        public decimal StartPrice { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDatetime { get; set; }
        
        public virtual Category Category { get; set; }

        public virtual LotState LotState { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
