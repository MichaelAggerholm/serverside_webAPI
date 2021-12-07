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
    public class EmployeesController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public EmployeesController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            _repositoryWrapper.EmployeeRepositoryWrapper.DisableLazyLoading();
            return Ok(await _repositoryWrapper.EmployeeRepositoryWrapper.FindAll());
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            _repositoryWrapper.EmployeeRepositoryWrapper.DisableLazyLoading();
            var employee = await _repositoryWrapper.EmployeeRepositoryWrapper.FindOne(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.PersonID)
            {
                return BadRequest();
            }

            await _repositoryWrapper.EmployeeRepositoryWrapper.Update(employee);

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            await _repositoryWrapper.EmployeeRepositoryWrapper.Create(employee);

            return CreatedAtAction("GetEmployee", new { id = employee.PersonID }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _repositoryWrapper.EmployeeRepositoryWrapper.FindOne(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _repositoryWrapper.EmployeeRepositoryWrapper.Delete(employee);

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (null != _repositoryWrapper.EmployeeRepositoryWrapper.FindOne(id));
        }
    }
}
