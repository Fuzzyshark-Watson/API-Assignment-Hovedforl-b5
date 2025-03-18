using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;
using ToDoApi.DataTransferObjects;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly EducationClassContext _context;

        public TeachersController(EducationClassContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDTO>>> GetTeachers()
        {
            var teachers = await _context.Teachers.ToListAsync();

            var teacherDtos = teachers.Select(t => new TeacherDTO
            {
                Id = t.Id,
                Name = t.Name
            });

            return Ok(teacherDtos);
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDTO>> GetTeacher(int id)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
            {
                return NotFound();
            }

            var teacherDto = new TeacherDTO
            {
                Id = teacher.Id,
                Name = teacher.Name
            };

            return Ok(teacherDto);
        }

        // POST: api/Teachers
        [HttpPost]
        public async Task<ActionResult<TeacherDTO>> PostTeacher(TeacherDTO teacherDto)
        {
            if (string.IsNullOrEmpty(teacherDto.Name))
            {
                return BadRequest("Teacher name is required.");
            }

            var teacher = new Teacher
            {
                Name = teacherDto.Name
            };

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            teacherDto.Id = teacher.Id;

            return CreatedAtAction(nameof(GetTeacher), new { id = teacherDto.Id }, teacherDto);
        }

        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, TeacherDTO teacherDto)
        {
            if (id != teacherDto.Id)
            {
                return BadRequest("Teacher ID mismatch.");
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            teacher.Name = teacherDto.Name;

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers
                .Include(t => t.SubjectTeachers)
                .Include(t => t.EducationClassTeachers)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
            {
                return NotFound();
            }

            // Explicitly remove related SubjectTeachers
            foreach (var subject in teacher.SubjectTeachers.ToList())
            {
                _context.Entry(subject).State = EntityState.Deleted;
            }
            // Explicitly remove related EducationClassTeachers
            foreach (var educationClass in teacher.EducationClassTeachers.ToList())
            {
                _context.Entry(educationClass).State = EntityState.Deleted;
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
