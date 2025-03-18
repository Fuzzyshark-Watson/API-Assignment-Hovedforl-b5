using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToDoApi.DataTransferObjects;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationClassesController : ControllerBase
    {
        private readonly EducationClassContext _context;


        public EducationClassesController(EducationClassContext context)
        {
            _context = context;
        }

        // GET: api/EducationClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationClass>>> GetEducationClasses()
        {
            return await _context.EducationClasses
                .Include(ec => ec.Day)
                .Include(ec => ec.EducationClassSubjects)
                .Include(ec => ec.EducationClassTeachers)
                .Include(ec => ec.EducationClassDays)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EducationClassDTO>> GetEducationClass(long id)
        {
            var educationClass = await _context.EducationClasses
                .Include(ec => ec.Day)
                .Include(ec => ec.EducationClassSubjects)
                    .ThenInclude(ecs => ecs.Subject)
                .Include(ec => ec.EducationClassTeachers)
                    .ThenInclude(ect => ect.Teacher)
                .FirstOrDefaultAsync(ec => ec.Id == id);

            if (educationClass == null)
            {
                return NotFound();
            }

            // Map to DTO
            var dto = new EducationClassDTO
            {
                Id = educationClass.Id,
                Hours = educationClass.Hours,
                DayId = educationClass.DayId,
                Day = new DayDTO { Id = educationClass.Day.Id, Date = educationClass.Day.Date },
                Subjects = educationClass.EducationClassSubjects
                    .Select(ecs => ecs.Subject != null
                        ? new SubjectDTO { Id = ecs.Subject.Id, Name = ecs.Subject.Name }
                        : new SubjectDTO { Id = 0, Name = "Unknown" }) // Default values
                    .ToList(),
                Teachers = educationClass.EducationClassTeachers
                .Select(ect => ect.Teacher != null
                    ? new TeacherDTO { Id = ect.Teacher.Id, Name = ect.Teacher.Name }
                    : new TeacherDTO { Id = 0, Name = "Unknown" }) // Default values
                .ToList()

            };

            return dto;
        }

        [HttpPost]
        public async Task<ActionResult<EducationClass>> CreateEducationClass(EducationClassCreateDTO dto)
        {
            // Retrieve referenced entities
            var day = await _context.Days.FindAsync(dto.DayId);
            if (day == null) return BadRequest("Invalid DayId");

            var subjects = await _context.Subjects
                .Where(s => dto.SubjectIds.Contains(s.Id))
                .ToListAsync();
            if (subjects.Count != dto.SubjectIds.Count) return BadRequest("Some SubjectIds are invalid");

            var teachers = await _context.Teachers
                .Where(t => dto.TeacherIds.Contains((int)t.Id))
                .ToListAsync();
            if (teachers.Count() != dto.TeacherIds.Count()) return BadRequest("Some TeacherIds are invalid");

            // Create new EducationClass
            var educationClass = new EducationClass
            {
                Hours = dto.Hours,
                Day = day,
                EducationClassSubjects = subjects
                    .Select(s => new EducationClassSubject { Subject = s })
                    .ToList(),
                EducationClassTeachers = teachers
                    .Select(t => new EducationClassTeacher { Teacher = t })
                    .ToList()
            };

            _context.EducationClasses.Add(educationClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEducationClass), new { id = educationClass.Id }, educationClass);
        }


        // PUT: api/EducationClasses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducationClass(long id, EducationClass educationClass)
        {
            if (id != educationClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(educationClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationClassExists(id))
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

        // DELETE: api/EducationClasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducationClass(long id)
        {
            var educationClass = await _context.EducationClasses
                .Include(ec => ec.EducationClassSubjects)
                .Include(ec => ec.EducationClassTeachers)
                .Include(ec => ec.EducationClassDays)
                .FirstOrDefaultAsync(ec => ec.Id == id);

            if (educationClass == null)
            {
                return NotFound();
            }

            // Explicitly remove related EducationClassSubjects
            foreach (var subject in educationClass.EducationClassSubjects.ToList())
            {
                _context.Entry(subject).State = EntityState.Deleted;
            }
            // Explicitly remove related EducationClassTeachers
            foreach (var teacher in educationClass.EducationClassTeachers.ToList())
            {
                _context.Entry(teacher).State = EntityState.Deleted;
            }
            // Explicitly remove related EducationClassDays
            foreach (var days in educationClass.EducationClassDays.ToList())
            {
                _context.Entry(days).State = EntityState.Deleted;
            }

            _context.EducationClasses.Remove(educationClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

/*        // POST: api/EducationClasses/5/SetDate
        [HttpPost("{id}/SetDate")]
        public async Task<IActionResult> SetDate(long id, [FromBody] DateTime date)
        {
            var educationClass = await _context.EducationClasses.FindAsync(id);

            if (educationClass == null)
            {
                return NotFound();
            }

            educationClass.SetDate(date);
            _context.Entry(educationClass).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

/*        // POST: api/EducationClasses/5/AddHours
        [HttpPost("{id}/AddHours")]
        public async Task<IActionResult> AddHours(long id)
        {
            var educationClass = await _context.EducationClasses.FindAsync(id);

            if (educationClass == null)
            {
                return NotFound();
            }

            educationClass.AddHours();
            _context.Entry(educationClass).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool EducationClassExists(long id)
        {
            return _context.EducationClasses.Any(e => e.Id == id);
        }
    }
}
