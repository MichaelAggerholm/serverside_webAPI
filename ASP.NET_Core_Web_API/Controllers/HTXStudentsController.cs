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
using ClassLibrary.DTO;
using Mapster;
using ASP.NET_Core_Web_API.Extensions;

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
        public async Task<ActionResult<IEnumerable<HTXStudent>>> GetHTXStudents(bool includeRelations = true,
                                                                                string UserName = "No Name")
        {
            var HTXStudentList = await _repositoryWrapper.HTXStudentRepositoryWrapper.FindAll();

            if ((false == includeRelations))
            {
                _repositoryWrapper.HTXStudentRepositoryWrapper.DisableLazyLoading();
            }
            else  // true == includeRelations && true == UseLazyLoading 
            {
                _repositoryWrapper.HTXStudentRepositoryWrapper.EnableLazyLoading();
            }

            List<HTXStudentDto> HTXStudentDtos = HTXStudentList.Adapt<HTXStudentDto[]>().ToList();

            return Ok(HTXStudentDtos);
        }

        // GET: api/HTXStudents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HTXStudent>> GetHTXStudent(int id,
                                                                  bool includeRelations = true,
                                                                  string UserName = "No Name")
        {
            if (false == includeRelations)
            {
                _repositoryWrapper.HTXStudentRepositoryWrapper.DisableLazyLoading();
            }
            else
            {
                _repositoryWrapper.HTXStudentRepositoryWrapper.EnableLazyLoading();
            }

            var HTXStudent_Object = await _repositoryWrapper.HTXStudentRepositoryWrapper.FindOne(id);

            if (HTXStudent_Object == null)
            {
                return NotFound();
            }
            else
            {

                HTXStudentDto HTXStudentDto_Object = HTXStudent_Object.Adapt<HTXStudentDto>();
                return Ok(HTXStudent_Object);
            }
        }

        // PUT: api/HTXStudents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> HTXStudent(int id,
                                                    [FromBody] HTXStudentDto HTXStudentDto_Object,
                                                    string UserName = "No Name")
        {
            if (id != HTXStudentDto_Object.SchoolID) //<--- idk
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var HTXStudentFromRepo = await _repositoryWrapper.HTXStudentRepositoryWrapper.FindOne(id);

            if (null == HTXStudentFromRepo)
            {
                return NotFound();
            }

            if (HTXStudentFromRepo.CloneData<HTXStudent>(HTXStudentDto_Object))
            {
                await _repositoryWrapper.HTXStudentRepositoryWrapper.Update(HTXStudentFromRepo);
            }

            return NoContent();
        }

        // POST: api/HTXStudents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> HTXStudent([FromBody] HTXStudentForSaveDto HTXStudentDto_Object,
                                                             string UserName = "No Name")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HTXStudent HTXStudent_Object = HTXStudentDto_Object.Adapt<HTXStudent>();

            await _repositoryWrapper.HTXStudentRepositoryWrapper.Create(HTXStudent_Object);

            return Ok(HTXStudent_Object.SchoolID);
        }

        // DELETE: api/HTXStudents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHTXStudent(int id,
                                                          string UserName = "No Name")
        {
            _repositoryWrapper.HTXStudentRepositoryWrapper.DisableLazyLoading();

            var HTXStudentFromRepo = await _repositoryWrapper.HTXStudentRepositoryWrapper.FindOne(id);

            if (null == HTXStudentFromRepo)
            {
                return NotFound();
            }

            await _repositoryWrapper.HTXStudentRepositoryWrapper.Delete(HTXStudentFromRepo);

            return NoContent();
        }

        private bool HTXStudentExists(int id)
        {
            return (null != _repositoryWrapper.HTXStudentRepositoryWrapper.FindOne(id));
        }
    }
}
