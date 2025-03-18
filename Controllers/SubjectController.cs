using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;
using ToDoApi.DataTransferObjects;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly EducationClassContext _context;

        public SubjectsController(EducationClassContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetSubjects()
        {
            var subjects = await _context.Subjects.ToListAsync();

            var subjectDtos = subjects.Select(s => new SubjectDTO
            {
                Id = s.Id,
                Name = s.Name
            });

            return Ok(subjectDtos);
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDTO>> GetSubject(long id)
        {
            var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);

            if (subject == null)
            {
                return NotFound();
            }

            var subjectDto = new SubjectDTO
            {
                Id = subject.Id,
                Name = subject.Name
            };

            return Ok(subjectDto);
        }

        // POST: api/Subjects
        [HttpPost]
        public async Task<ActionResult<SubjectDTO>> PostSubject(SubjectDTO subjectDto)
        {
            if (string.IsNullOrEmpty(subjectDto.Name))
            {
                return BadRequest("Subject name is required.");
            }

            var subject = new Subject
            {
                Name = subjectDto.Name
            };

            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            subjectDto.Id = subject.Id;

            return CreatedAtAction(nameof(GetSubject), new { id = subjectDto.Id }, subjectDto);
        }

        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(long id, SubjectDTO subjectDto)
        {
            if (id != subjectDto.Id)
            {
                return BadRequest("Subject ID mismatch.");
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            subject.Name = subjectDto.Name;

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(long id)
        {
            var subject = await _context.Subjects
                .Include(s => s.CourseSubjects)
                .Include(s => s.SubjectTeachers)
                .Include(s => s.EducationClassSubjects)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subject == null)
            {
                return NotFound();
            }

            // Explicitly remove related CourseSubjects
            foreach (var course in subject.CourseSubjects.ToList())
            {
                _context.Entry(course).State = EntityState.Deleted;
            }
            // Explicitly remove related SubjectTeachers
            foreach (var teacher in subject.SubjectTeachers.ToList())
            {
                _context.Entry(teacher).State = EntityState.Deleted;
            }
            // Explicitly remove related EducationClassSubjects
            foreach (var educationClass in subject.EducationClassSubjects.ToList())
            {
                _context.Entry(educationClass).State = EntityState.Deleted;
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Subjects/5/Refresh
        [HttpPost("{id}/Refresh")]
        public async Task<IActionResult> RefreshSubject(long id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            subject.Refresh();
            _context.Entry(subject).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectExists(long id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}
