
﻿[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookingController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ CREATE BOOKING
    [HttpPost]
    public async Task<IActionResult> CreateBooking(CreateBookingDto dto)
    {
        // 🔹 In real app → get from JWT
        int userId = 1;

        var room = await _context.Rooms.FindAsync(dto.RoomId);

        if (room == null)
            return NotFound("Room not found");

        // 🔥 CHECK AVAILABILITY (DATE OVERLAP LOGIC)
        bool isBooked = await _context.Bookings.AnyAsync(b =>
            b.RoomId == dto.RoomId &&
            b.Status == "Confirmed" &&
            dto.CheckInDate < b.CheckOutDate &&
            dto.CheckOutDate > b.CheckInDate
        );

        if (isBooked)
            return BadRequest("Room not available for selected dates");

        // 🔥 CALCULATE PRICE
        int days = (dto.CheckOutDate - dto.CheckInDate).Days;

        if (days <= 0)
            return BadRequest("Invalid dates");

        decimal totalAmount = days * room.Price;

        // 🔥 CREATE BOOKING
        var booking = new Booking
        {
            UserId = userId,
            RoomId = dto.RoomId,
            CheckInDate = dto.CheckInDate,
            CheckOutDate = dto.CheckOutDate,
            TotalAmount = totalAmount,
            Status = "Confirmed"
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Booking Confirmed",
            bookingId = booking.BookingId
        });
    }

    // ✅ GET USER BOOKINGS
    [HttpGet("my/{userId}")]
    public async Task<IActionResult> GetMyBookings(int userId)
    {
        var bookings = await _context.Bookings
            .Include(b => b.Room)
            .ThenInclude(r => r.Hotel)
            .Where(b => b.UserId == userId)
            .ToListAsync();

        return Ok(bookings);
    }

    // ✅ CANCEL BOOKING
    [HttpPut("cancel/{id}")]
    public async Task<IActionResult> CancelBooking(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);

        if (booking == null)
            return NotFound("Booking not found");

        booking.Status = "Cancelled";

        await _context.SaveChangesAsync();

        return Ok("Booking cancelled successfully");
    }
}
﻿
