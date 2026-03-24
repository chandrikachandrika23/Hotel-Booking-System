namespace HotelBookingAPI.DTOs
{
    public class CreateBookingDto
    {
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int Guests { get; set; }
    }
}