namespace HotelBookingAPi.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Amenities { get; set; }

        // Navigation
        public ICollection<Room> Rooms { get; set; }
    }
}
