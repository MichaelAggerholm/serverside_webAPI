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
    public class HTXStudentsController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public HTXStudentsController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: api/HTXStudents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HTXStudent>>> GetHTXStudents()
        {
            _repositoryWrapper.HTXStudentRepositoryWrapper.DisableLazyLoading();
            return Ok(await _repositoryWrapper.HTXStudentRepositoryWrapper.FindAll());
        }

        // GET: api/HTXStudents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HTXStudent>> GetHTXStudent(int id)
        {
            var hTXStudent = await _repositoryWrapper.HTXStudentRepositoryWrapper.FindOne(id);

            if (hTXStudent == null)
            {
                return NotFound();
            }

            return hTXStudent;
        }

        // PUT: api/HTXStudents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> HTXStudent(int id, HTXStudent htxstudent)
        {
            if (id != htxstudent.PersonID)
            {
                return BadRequest();
            }

            await _repositoryWrapper.HTXStudentRepositoryWrapper.Update(htxstudent);

            return NoContent();
        }

        // POST: api/HTXStudents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> HTXStudent(HTXStudent htxstudent)
        {
            await _repositoryWrapper.HTXStudentRepositoryWrapper.Create(htxstudent);

            return CreatedAtAction("GetHTXStudent", new { id = htxstudent.PersonID }, htxstudent);
        }

        // DELETE: api/HTXStudents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHTXStudent(int id)
        {
            var htxstudent = await _repositoryWrapper.HTXStudentRepositoryWrapper.FindOne(id);
            if (htxstudent == null)
            {
                return NotFound();
            }

            await _repositoryWrapper.HTXStudentRepositoryWrapper.Delete(htxstudent);

            return NoContent();
        }

        private bool HTXStudentExists(int id)
        {
            return (null != _repositoryWrapper.HTXStudentRepositoryWrapper.FindOne(id));
        }
    }
}
