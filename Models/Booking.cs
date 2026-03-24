namespace HotelBookingAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int Guests { get; set; }
        public decimal TotalPrice { get; set; }

        public string BookingStatus { get; set; } = "Confirmed";
        public string BookingNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}