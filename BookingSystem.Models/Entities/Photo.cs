using System;

namespace BookingSystem.Models.Entities
{
    public class Photo
    {
        public int PhotoId { get; set; }

        public string Title { get; set; }

        public byte[] Image { get; set; }

        public int SiteId { get; set; }

        public Site Site { get; set; }
    }
}
