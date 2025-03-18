using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;
using ToDoApi.DataTransferObjects;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysController : ControllerBase
    {
        private readonly EducationClassContext _context;

        public DaysController(EducationClassContext context)
        {
            _context = context;
        }

        // GET: api/Days
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DayDTO>>> GetDays()
        {
            var days = await _context.Days
                .Include(d => d.EducationClassDays)
                .ThenInclude(ecd => ecd.EducationClass)
                .ToListAsync();

            var dayDtos = days.Select(d => new DayDTO
            {
                Id = d.Id,
                Name = $"Day {d.Id}",
                Date = d.Date
            });

            return Ok(dayDtos);
        }

        // GET: api/Days/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DayDTO>> GetDay(long id)
        {
            var day = await _context.Days
                .Include(d => d.EducationClassDays)
                .ThenInclude(ecd => ecd.EducationClass)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (day == null)
            {
                return NotFound();
            }

            var dayDto = new DayDTO
            {
                Id = day.Id,
                Name = $"Day {day.Id}",
                Date = day.Date
            };

            return Ok(dayDto);
        }

        // POST: api/Days
        [HttpPost]
        public async Task<ActionResult<DayDTO>> PostDay(DayDTO dayDto)
        {
            if (dayDto == null)
            {
                return BadRequest("DayDTO cannot be null.");
            }

            var day = new Day
            {
                Date = dayDto.Date
            };

            _context.Days.Add(day);
            await _context.SaveChangesAsync();

            var createdDayDto = new DayDTO
            {
                Id = day.Id,
                Name = $"Day {day.Id}",
                Date = day.Date
            };

            return CreatedAtAction(nameof(GetDay), new { id = createdDayDto.Id }, createdDayDto);
        }

        // PUT: api/Days/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDay(long id, DayDTO dayDto)
        {
            if (id != dayDto.Id)
            {
                return BadRequest("Day ID mismatch.");
            }

            var day = await _context.Days.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }

            day.Date = dayDto.Date;

            _context.Entry(day).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Days/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDay(long id)
        {
            var day = await _context.Days.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }

            _context.Days.Remove(day);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Days/5/SpentHours
        [HttpGet("{id}/SpentHours")]
        public async Task<ActionResult<long>> GetSpentHours(long id)
        {
            var day = await _context.Days
                .Include(d => d.EducationClassDays)
                .ThenInclude(ecd => ecd.EducationClass)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (day == null)
            {
                return NotFound();
            }

            return day.GetSpentHours();
        }

        // GET: api/Days/5/MaxHours
        [HttpGet("{id}/MaxHours")]
        public async Task<ActionResult<long?>> GetMaxHours(long id)
        {
            var day = await _context.Days.FindAsync(id);

            if (day == null)
            {
                return NotFound();
            }

            return day.GetMaxHours();
        }

        private bool DayExists(long id)
        {
            return _context.Days.Any(e => e.Id == id);
        }
    }
}
