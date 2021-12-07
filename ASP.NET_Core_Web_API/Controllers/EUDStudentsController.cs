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
    public class EUDStudentsController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public EUDStudentsController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: api/EUDStudents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EUDStudent>>> GetEUDStudents()
        {
            _repositoryWrapper.EUDStudentRepositoryWrapper.DisableLazyLoading();
            return Ok(await _repositoryWrapper.EUDStudentRepositoryWrapper.FindAll());
        }

        // GET: api/EUDStudents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EUDStudent>> GetEUDStudent(int id)
        {
            _repositoryWrapper.EUDStudentRepositoryWrapper.DisableLazyLoading();
            var eUDStudent = await _repositoryWrapper.EUDStudentRepositoryWrapper.FindOne(id);

            if (eUDStudent == null)
            {
                return NotFound();
            }

            return eUDStudent;
        }

        // PUT: api/EUDStudents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEUDStudent(int id, EUDStudent eUDStudent)
        {
            if (id != eUDStudent.PersonID)
            {
                return BadRequest();
            }

            await _repositoryWrapper.EUDStudentRepositoryWrapper.Update(eUDStudent);

            return NoContent();
        }

        // POST: api/EUDStudents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EUDStudent>> PostEUDStudent(EUDStudent eUDStudent)
        {
            await _repositoryWrapper.EUDStudentRepositoryWrapper.Create(eUDStudent);

            return CreatedAtAction("GetEUDStudent", new { id = eUDStudent.PersonID }, eUDStudent);
        }

        // DELETE: api/EUDStudents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEUDStudent(int id)
        {
            var eUDStudent = await _repositoryWrapper.EmployeeRepositoryWrapper.FindOne(id);
            if (eUDStudent == null)
            {
                return NotFound();
            }

            await _repositoryWrapper.EmployeeRepositoryWrapper.Delete(eUDStudent);

            return NoContent();
        }

        private bool EUDStudentExists(int id)
        {
            return (null != _repositoryWrapper.EmployeeRepositoryWrapper.FindOne(id));
        }
    }
}
