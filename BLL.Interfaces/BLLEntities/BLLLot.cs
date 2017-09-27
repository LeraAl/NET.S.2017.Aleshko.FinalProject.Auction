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

        public int StateId { get; set; }
        //public string State { get; set; } ToAsk

        public int OwnerId { get; set; }
        //public string OwnerName { get; set; } ToAsk

        public int CategoryId { get; set; }
        //public string Category { get; set; } ToAsk


    }
}