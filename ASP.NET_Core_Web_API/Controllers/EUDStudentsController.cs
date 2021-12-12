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
    public class EUDStudentsController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public EUDStudentsController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: api/EUDStudents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EUDStudent>>> GetEUDStudents(bool includeRelations = true,
                                                                                string UserName = "No Name")
        {
            var EUDStudentList = await _repositoryWrapper.EUDStudentRepositoryWrapper.FindAll();

            if ((false == includeRelations))
            {
                _repositoryWrapper.EUDStudentRepositoryWrapper.DisableLazyLoading();
            }
            else  // true == includeRelations && true == UseLazyLoading 
            {
                _repositoryWrapper.EUDStudentRepositoryWrapper.EnableLazyLoading();
            }

            List<EUDStudentDto> EUDStudentDtos = EUDStudentList.Adapt<EUDStudentDto[]>().ToList();

            return Ok(EUDStudentDtos);
        }

        // GET: api/EUDStudents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EUDStudent>> GetEUDStudent(int id,
                                                                  bool includeRelations = true,
                                                                  string UserName = "No Name")
        {
            if (false == includeRelations)
            {
                _repositoryWrapper.EUDStudentRepositoryWrapper.DisableLazyLoading();
            }
            else
            {
                _repositoryWrapper.EUDStudentRepositoryWrapper.EnableLazyLoading();
            }

            var EUDStudent_Object = await _repositoryWrapper.EUDStudentRepositoryWrapper.FindOne(id);

            if (EUDStudent_Object == null)
            {
                return NotFound();
            }
            else
            {

                EUDStudentDto EUDStudentDto_Object = EUDStudent_Object.Adapt<EUDStudentDto>();
                return Ok(EUDStudent_Object);
            }
        }

        // PUT: api/EUDStudents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEUDStudent(int id,
                                                       [FromBody] EUDStudentDto EUDStudentDto_Object,
                                                       string UserName = "No Name")
        {
            if (id != EUDStudentDto_Object.SchoolID) //<--- idk
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var EUDStudentFromRepo = await _repositoryWrapper.EUDStudentRepositoryWrapper.FindOne(id);

            if (null == EUDStudentFromRepo)
            {
                return NotFound();
            }

            if (EUDStudentFromRepo.CloneData<EUDStudent>(EUDStudentDto_Object))
            {
                await _repositoryWrapper.EUDStudentRepositoryWrapper.Update(EUDStudentFromRepo);
            }

            return NoContent();
        }

        // POST: api/EUDStudents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EUDStudent>> PostEUDStudent([FromBody] EUDStudentForSaveDto EUDStudentDto_Object,
                                                                   string UserName = "No Name")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EUDStudent EUDStudent_Object = EUDStudentDto_Object.Adapt<EUDStudent>();

            await _repositoryWrapper.EUDStudentRepositoryWrapper.Create(EUDStudent_Object);

            return Ok(EUDStudent_Object.SchoolID);
        }

        // DELETE: api/EUDStudents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEUDStudent(int id,
                                                          string UserName = "No Name")
        {
            _repositoryWrapper.EUDStudentRepositoryWrapper.DisableLazyLoading();

            var EUDStudentFromRepo = await _repositoryWrapper.EUDStudentRepositoryWrapper.FindOne(id);

            if (null == EUDStudentFromRepo)
            {
                return NotFound();
            }

            await _repositoryWrapper.EUDStudentRepositoryWrapper.Delete(EUDStudentFromRepo);

            return NoContent();
        }

        private bool EUDStudentExists(int id)
        {
            return (null != _repositoryWrapper.EUDStudentRepositoryWrapper.FindOne(id));
        }
    }
}
