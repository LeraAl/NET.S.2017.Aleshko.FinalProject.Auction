using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Lot
{
    public class LotCreateModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public byte[] Image { get; set; }

        [Required]
        [Display(Name = "Start Price")]
        public decimal StartPrice { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}