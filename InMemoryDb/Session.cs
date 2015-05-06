using System;

namespace InMemoryDb
{
    public class Session
    {
        public int Id { get; set; }
        public short AccountId { get; set; }
        public DateTime StartDateTime { get; set; }
    }
}