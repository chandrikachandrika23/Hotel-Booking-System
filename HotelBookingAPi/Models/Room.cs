using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingAPi.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }   // FK
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        // Navigation
        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
