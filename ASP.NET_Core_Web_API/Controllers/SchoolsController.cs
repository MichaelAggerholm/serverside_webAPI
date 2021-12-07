using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassLibrary.Content;
using ClassLibrary.Models;
using ClassLibrary.Interfaces;

namespace ASP.NET_Core_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private IRepositoryWrapper _repositoryWrapper;

        public SchoolsController(DatabaseContext context, IRepositoryWrapper repositoryWrapper)
        {
            _context = context;
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: api/Schools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetSchools()
        {
            _repositoryWrapper.EmployeeRepositoryWrapper.DisableLazyLoading();
            return Ok(await _repositoryWrapper.EmployeeRepositoryWrapper.FindAll());
            //return await _context.Schools.ToListAsync();
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            return school;
        }

        // PUT: api/Schools/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool(int id, School school)
        {
            if (id != school.SchoolID)
            {
                return BadRequest();
            }

            _context.Entry(school).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(id))
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

        // POST: api/Schools
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<School>> PostSchool(School school)
        {
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchool", new { id = school.SchoolID }, school);
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolExists(int id)
        {
            return _context.Schools.Any(e => e.SchoolID == id);
        }
    }
}
