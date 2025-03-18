using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.DataTransferObjects;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly EducationClassContext _context;

        public CourseController(EducationClassContext context)
        {
            _context = context;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            var courses = await _context.Courses
                .Select(c => new CourseDTO
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return Ok(courses);
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(long id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound(new { message = $"Course with ID {id} not found." });
            }

            var courseDto = new CourseDTO
            {
                Id = course.Id,
                Name = course.Name
            };

            return Ok(courseDto);
        }

        // POST: api/Course
        [HttpPost]
        public async Task<ActionResult<CourseDTO>> CreateCourse(CourseDTO courseDto)
        {
            if (string.IsNullOrEmpty(courseDto.Name))
            {
                return BadRequest("Course name is required.");
            }

            var course = new Course
            {
                Name = courseDto.Name
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var createdCourseDto = new CourseDTO
            {
                Id = course.Id,
                Name = course.Name
            };

            return CreatedAtAction(nameof(GetCourse), new { id = createdCourseDto.Id }, createdCourseDto);
        }

        // PUT: api/Course/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(long id, CourseDTO courseDto)
        {
            if (id != courseDto.Id)
            {
                return BadRequest("Course ID mismatch.");
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound(new { message = $"Course with ID {id} not found." });
            }

            course.Name = courseDto.Name;

            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(long id)
        {
            var course = await _context.Courses
                .Include(c => c.CourseSubjects)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound(new { message = $"Course with ID {id} not found." });
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(long id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
