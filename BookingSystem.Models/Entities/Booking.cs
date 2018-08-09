using System;

namespace BookingSystem.Models.Entities
{
    public class Booking
    {
        public Booking()
        {
        }

        public int BookingId { get; set; }

        public string TenantId { get; set; }

        public AppUser Tenant { get; set; }

        public int SiteId { get; set; }

        public Site Site { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
