using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using docker_demo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace docker_demo.Controllers
{
    [Produces("application/json")]
    [Route("api/Student")]
    public class StudentController : Controller
    {
        private readonly DemoDb _context;
        public StudentController(DemoDb context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public IEnumerable<Student> GetStudent()
        {
            return _context.Students;
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
            if (Student == null)
            {
                return NotFound();
            }
            return Ok(Student);
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent([FromRoute] int id, [FromBody] Student Student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != Student.Id)
            {
                return BadRequest();
            }
            _context.Entry(Student).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Student
        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] Student Student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetStudent", new { id = Student.Id }, Student);
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
            if (Student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(Student);
            await _context.SaveChangesAsync();
            return Ok(Student);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}