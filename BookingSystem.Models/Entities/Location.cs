using System;

namespace BookingSystem.Models.Entities
{
    public class Location
    {
        public int LocationId { get; set; }

        public string PostalCode { get; set; }

        public string Address { get; set; }

        public string Region { get; set; }

        public string TownCity { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public int SiteId { get; set; }

        public Site Site { get; set; }
    }
}
