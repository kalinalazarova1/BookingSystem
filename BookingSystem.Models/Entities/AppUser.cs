using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookingSystem.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            this.Sites = new HashSet<Site>();
            this.Bookings = new HashSet<Booking>();
        }

        public HashSet<Site> Sites { get; set; }

        public HashSet<Booking> Bookings { get; set; }
    }
}
