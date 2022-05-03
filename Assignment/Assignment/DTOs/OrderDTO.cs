using System;

namespace Assignment.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string ContactName { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? ExtendedPrice { get; set; }

    }
}
