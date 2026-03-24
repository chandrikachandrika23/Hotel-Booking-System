
﻿using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingAPi.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public string UserId { get; set; }   // FK (IdentityUser)
        public int RoomId { get; set; }      // FK

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        // Navigation
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }
    }
}

﻿