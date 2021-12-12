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
    public class SchoolsController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public SchoolsController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: api/Schools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetSchools(bool includeRelations = true,
                                                                        string UserName = "No Name")
        {
            var schoolList = await _repositoryWrapper.SchoolRepositoryWrapper.FindAll();

            if ((false == includeRelations))
            {
                _repositoryWrapper.SchoolRepositoryWrapper.DisableLazyLoading();
            }
            else  // true == includeRelations && true == UseLazyLoading 
            {
                _repositoryWrapper.SchoolRepositoryWrapper.EnableLazyLoading();
            }

            List<SchoolDto> SchoolDtos = schoolList.Adapt<SchoolDto[]>().ToList();

            return Ok(SchoolDtos);

            //return await _context.Schools.ToListAsync();
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchool(int id,
                                                          bool includeRelations = true,
                                                          string UserName = "No Name")
        {
            if (false == includeRelations)
            {
                _repositoryWrapper.SchoolRepositoryWrapper.DisableLazyLoading();
            }
            else
            {
                _repositoryWrapper.SchoolRepositoryWrapper.EnableLazyLoading();
            }

            var School_Object = await _repositoryWrapper.SchoolRepositoryWrapper.FindOne(id);

            if (School_Object == null)
            {
                return NotFound();
            }
            else
            {

                SchoolDto SchoolDto_Object = School_Object.Adapt<SchoolDto>();
                return Ok(SchoolDto_Object);
            }
        }

        // PUT: api/Schools/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchool(int id,
                                                      [FromBody] SchoolDto SchoolDto_Object,
                                                      string UserName = "No Name")
        {
            if (id != SchoolDto_Object.SchoolID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var SchoolFromRepo = await _repositoryWrapper.SchoolRepositoryWrapper.FindOne(id);

            if (null == SchoolFromRepo)
            {
                return NotFound();
            }

            if (SchoolFromRepo.CloneData<School>(SchoolDto_Object))
            {
                await _repositoryWrapper.SchoolRepositoryWrapper.Update(SchoolFromRepo);
            }

            return NoContent();
        }

        // POST: api/Schools
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<School>> CreateSchool([FromBody] SchoolForSaveDto SchoolDto_Object,
                                                             string UserName = "No Name")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            School School_Object = SchoolDto_Object.Adapt<School>();

            await _repositoryWrapper.SchoolRepositoryWrapper.Create(School_Object);

            return Ok(School_Object.SchoolID);
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id,
                                                       string UserName = "No Name")
        {
            _repositoryWrapper.SchoolRepositoryWrapper.DisableLazyLoading();

            var SchoolFromRepo = await _repositoryWrapper.SchoolRepositoryWrapper.FindOne(id);

            if (null == SchoolFromRepo)
            {
                return NotFound();
            }

            await _repositoryWrapper.SchoolRepositoryWrapper.Delete(SchoolFromRepo);

            return NoContent();
        }

        private bool SchoolExists(int id)
        {
            return (null != _repositoryWrapper.SchoolRepositoryWrapper.FindOne(id));
        }
    }
}
