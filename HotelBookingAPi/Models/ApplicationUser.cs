namespace HotelBookingAPi.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        // Navigation
        public ICollection<Booking> Bookings { get; set; }
    }
}
