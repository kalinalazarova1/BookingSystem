using System;

namespace BookingSystem.Models.Entities
{
    public class Price
    {
        public int PriceId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public decimal Amount { get; set; }

        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        public int SiteId { get; set; }

        public Site Site { get; set; }
    }
}
