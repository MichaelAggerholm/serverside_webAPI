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
using Mapster;
using ClassLibrary.DTO;
using ASP.NET_Core_Web_API.Extensions;

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
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees(bool includeRelations = true,
                                                                            string UserName = "No Name")
        {
            var EmployeeList = await _repositoryWrapper.EmployeeRepositoryWrapper.FindAll();

            if ((false == includeRelations))
            {
                _repositoryWrapper.EmployeeRepositoryWrapper.DisableLazyLoading();
            }
            else  // true == includeRelations && true == UseLazyLoading 
            {
                _repositoryWrapper.EmployeeRepositoryWrapper.EnableLazyLoading();
            }

            List<EmployeeDto> EmployeeDtos = EmployeeList.Adapt<EmployeeDto[]>().ToList();

            return Ok(EmployeeDtos);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id,
                                                              bool includeRelations = true,
                                                              string UserName = "No Name")
        {
            if (false == includeRelations)
            {
                _repositoryWrapper.EmployeeRepositoryWrapper.DisableLazyLoading();
            }
            else
            {
                _repositoryWrapper.EmployeeRepositoryWrapper.EnableLazyLoading();
            }

            var Employee_Object = await _repositoryWrapper.EmployeeRepositoryWrapper.FindOne(id);

            if (Employee_Object == null)
            {
                return NotFound();
            }
            else
            {

                SchoolDto EmployeeDto_Object = Employee_Object.Adapt<SchoolDto>();
                return Ok(Employee_Object);
            }
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id,
                                                     [FromBody] EmployeeDto EmployeeDto_Object,
                                                     string UserName = "No Name")
        {
            if (id != EmployeeDto_Object.SchoolID) //<--- idk
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var EmployeeFromRepo = await _repositoryWrapper.EmployeeRepositoryWrapper.FindOne(id);

            if (null == EmployeeFromRepo)
            {
                return NotFound();
            }

            if (EmployeeFromRepo.CloneData<Employee>(EmployeeDto_Object))
            {
                await _repositoryWrapper.EmployeeRepositoryWrapper.Update(EmployeeFromRepo);
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee([FromBody] EmployeeForSaveDto EmployeeDto_Object,
                                                                string UserName = "No Name")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee Employee_Object = EmployeeDto_Object.Adapt<Employee>();

            await _repositoryWrapper.EmployeeRepositoryWrapper.Create(Employee_Object);

            return Ok(Employee_Object.PersonID);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id,
                                                        string UserName = "No Name")
        {
            _repositoryWrapper.EmployeeRepositoryWrapper.DisableLazyLoading();

            var EmployeeFromRepo = await _repositoryWrapper.EmployeeRepositoryWrapper.FindOne(id);

            if (null == EmployeeFromRepo)
            {
                return NotFound();
            }

            await _repositoryWrapper.EmployeeRepositoryWrapper.Delete(EmployeeFromRepo);

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (null != _repositoryWrapper.EmployeeRepositoryWrapper.FindOne(id));
        }
    }
}