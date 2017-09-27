namespace BLL.Interfaces.BLLEntities
{
    public class BLLLot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public decimal StartPrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public string Status { get; set; }

        public int OwnerId { get; set; }
        public string OwnerName { get; set; }

        public int CategoryId { get; set; }
        public string Category { get; set; }


    }
}