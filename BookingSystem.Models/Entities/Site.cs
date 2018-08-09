using System;
using System.Collections.Generic;

namespace BookingSystem.Models.Entities
{
    public class Site
    {
        public Site()
        {
            this.Prices = new HashSet<Price>();
            this.Bookings = new HashSet<Booking>();
            this.Photos = new HashSet<Photo>();
        }

        public int SiteId { get; set; }

        public Location Location { get; set; }

        public string OwnerId { get; set; }

        public AppUser Owner { get; set; }

        public HashSet<Price> Prices { get; set; }

        public HashSet<Booking> Bookings { get; set; }

        public HashSet<Photo> Photos { get; set; }
    }
}
