using System.Diagnostics.CodeAnalysis;

namespace ORM
{
	public class Favorite
	{
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

		public int Id { get; set; }

		public int UserId { get; set; }

		public int LotId { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual User User { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual Lot Lot { get; set; }
	}
}