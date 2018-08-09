using System;

namespace BookingSystem.Models.Entities
{
    public class Currency
    {
        public int CurrencyId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Symbol { get; set; }
    }
}
