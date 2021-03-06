﻿using System;

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
        public string State { get; set; }

        public int OwnerId { get; set; }
        public BLLUser Owner { get; set; }

        public int CategoryId { get; set; }
        public BLLCategory Category { get; set; }

        public DateTime StartDatetime { get; set; }
    }
}