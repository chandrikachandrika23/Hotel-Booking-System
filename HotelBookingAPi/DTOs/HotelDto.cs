namespace HotelBookingAPi.DTOs
{
    public class HotelDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Amenities { get; set; } = string.Empty;
    }

    public class CreateHotelDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Amenities { get; set; } = string.Empty;
    }

    public class UpdateHotelDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Amenities { get; set; } = string.Empty;
    }
}