[Table("Bookings")]
public class Booking
{
    [Key]
    public int BookingId { get; set; }

    public int UserId { get; set; }
    public int RoomId { get; set; }

    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } // Confirmed / Cancelled

    // Navigation
    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("RoomId")]
    public Room Room { get; set; }
}