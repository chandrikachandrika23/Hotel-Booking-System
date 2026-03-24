using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBookingApp.Data;
using HotelBookingApp.Models;
using HotelBookingApp.DTOs;

namespace HotelBookingApp.Controllers
{
    [ApiController]
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
            // Check availability
            var isBooked = await _context.Bookings.AnyAsync(b =>
                b.RoomId == dto.RoomId &&
                dto.CheckInDate < b.CheckOutDate &&
                dto.CheckOutDate > b.CheckInDate
            );

            if (isBooked)
            {
                return BadRequest("Room not available for selected dates");
            }

            // Get room price
            var room = await _context.Rooms.FindAsync(dto.RoomId);

            if (room == null)
            {
                return NotFound("Room not found");
            }

            int days = (dto.CheckOutDate - dto.CheckInDate).Days;
            decimal totalPrice = room.Price * days;

            // Generate booking number
            string bookingNumber = "BOOK" + DateTime.Now.Ticks;

            var booking = new Booking
            {
                UserId = dto.UserId,
                HotelId = dto.HotelId,
                RoomId = dto.RoomId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                Guests = dto.Guests,
                TotalPrice = totalPrice,
                BookingNumber = bookingNumber,
                BookingStatus = "Confirmed"
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }

        // 📜 GET USER BOOKINGS
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBookings(int userId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return Ok(bookings);
        }

        // ❌ CANCEL BOOKING
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound("Booking not found");
            }

            booking.BookingStatus = "Cancelled";
            await _context.SaveChangesAsync();

            return Ok("Booking cancelled successfully");
        }
    }
}