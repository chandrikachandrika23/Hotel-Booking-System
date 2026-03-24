using HotelBookingAPi.Data;
using HotelBookingAPi.DTOs;
using HotelBookingAPi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HotelsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetAllHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();

            var hotelDtos = hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Location = h.Location,
                Amenities = h.Amenities
            }).ToList();

            return Ok(hotelDtos);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotelById(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
                return NotFound(new { message = "Hotel not found" });

            var hotelDto = new HotelDto
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Location = hotel.Location,
                Amenities = hotel.Amenities
            };

            return Ok(hotelDto);
        }

        // GET: api/Hotels/search?location=mumbai
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<HotelDto>>> SearchHotels([FromQuery] string? location)
        {
            var query = _context.Hotels.AsQueryable();

            if (!string.IsNullOrEmpty(location))
                query = query.Where(h => h.Location.ToLower().Contains(location.ToLower()));

            var hotels = await query.ToListAsync();

            var hotelDtos = hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Location = h.Location,
                Amenities = h.Amenities
            }).ToList();

            return Ok(hotelDtos);
        }

        // POST: api/Hotels
        [HttpPost]
        public async Task<ActionResult<HotelDto>> CreateHotel(CreateHotelDto dto)
        {
            var hotel = new Hotel
            {
                Name = dto.Name,
                Location = dto.Location,
                Amenities = dto.Amenities
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            var hotelDto = new HotelDto
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Location = hotel.Location,
                Amenities = hotel.Amenities
            };

            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId }, hotelDto);
        }

        // PUT: api/Hotels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, UpdateHotelDto dto)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
                return NotFound(new { message = "Hotel not found" });

            hotel.Name = dto.Name;
            hotel.Location = dto.Location;
            hotel.Amenities = dto.Amenities;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Hotel updated successfully" });
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
                return NotFound(new { message = "Hotel not found" });

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Hotel deleted successfully" });
        }
    }
}