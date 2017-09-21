namespace DAL.Interfaces.DTO
{
    public class DALLot: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public int? StateId { get; set; }

        public byte[] Image { get; set; }

        public byte[] Description { get; set; }

        public int OwnerId { get; set; }

        public decimal StartPrice { get; set; }
    }
}