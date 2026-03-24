using HotelBookingAPi.Data;
using HotelBookingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 1. Get All Rooms (User)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Hotel)
                .ToListAsync();

            return Ok(rooms);
        }

        // ✅ 2. Get Room by Id (User)
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
                return NotFound("Room not found");

            return Ok(room);
        }

        // ✅ 3. Get Rooms by HotelId
        [HttpGet("hotel/{hotelId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoomsByHotel(int hotelId)
        {
            var rooms = await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();

            return Ok(rooms);
        }

        // ✅ 4. Create Room (Admin)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoom(Room room)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Check Hotel Exists
                var hotelExists = await _context.Hotels
                    .AnyAsync(h => h.HotelId == room.HotelId);

                if (!hotelExists)
                    return BadRequest("Invalid HotelId");

                // Default availability
                room.IsAvailable = true;

                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRoom),
                    new { id = room.RoomId }, room);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // ✅ 5. Update Room (Admin)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoom(int id, Room updatedRoom)
        {
            try
            {
                if (id != updatedRoom.RoomId)
                    return BadRequest("Room ID mismatch");

                var room = await _context.Rooms.FindAsync(id);

                if (room == null)
                    return NotFound("Room not found");

                // Update fields
                room.Category = updatedRoom.Category;
                room.Price = updatedRoom.Price;
                room.IsAvailable = updatedRoom.IsAvailable;
                room.HotelId = updatedRoom.HotelId;

                await _context.SaveChangesAsync();

                return Ok(room);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // ✅ 6. Delete Room (Admin)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                var room = await _context.Rooms.FindAsync(id);

                if (room == null)
                    return NotFound("Room not found");

                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();

                return Ok("Room deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // ✅ 7. Check Availability (User)
        [HttpGet("availability/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckAvailability(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
                return NotFound("Room not found");

            // Dynamic availability based on bookings
            var activeBookings = await _context.Bookings
                .CountAsync(b => b.RoomId == id);

            bool available = activeBookings == 0;

            return Ok(new
            {
                room.RoomId,
                IsAvailable = available
            });
        }
    }
}
