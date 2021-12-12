using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassLibrary;
using ClassLibrary.Content;
using ClassLibrary.Interfaces;
using ClassLibrary.DTO;
using Mapster;
using ASP.NET_Core_Web_API.Extensions;

namespace ASP.NET_Core_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public PersonsController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons(bool includeRelations = true,
                                                                           bool UseLazyLoading = true,
                                                                           string UserName = "No Name")
        {
            var PersonList = await _repositoryWrapper.PersonRepositoryWrapper.FindAll();

            if ((false == includeRelations) || (false == UseLazyLoading))
            {
                _repositoryWrapper.PersonRepositoryWrapper.DisableLazyLoading();
            }
            else  // true == includeRelations && true == UseLazyLoading 
            {
                _repositoryWrapper.PersonRepositoryWrapper.EnableLazyLoading();
            }

            if (true == UseLazyLoading)
            {
                PersonList = await _repositoryWrapper.PersonRepositoryWrapper.FindAll();
            }
            else
            {
                PersonList = await _repositoryWrapper.PersonRepositoryWrapper.GetAllPersons(includeRelations) as IEnumerable<Person>; //as IQueryable<City>;
            }

            List<PersonDto> PersonDtos;

            PersonDtos = PersonList.Adapt<PersonDto[]>().ToList();

            return Ok(PersonDtos);

            //return await _context.Persons.ToListAsync();
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id,
                                                          bool includeRelations = true,
                                                          string UserName = "No Name")
        {
            if (false == includeRelations)
            {
                _repositoryWrapper.PersonRepositoryWrapper.DisableLazyLoading();
            }
            else
            {
                _repositoryWrapper.PersonRepositoryWrapper.EnableLazyLoading();
            }

            var Person_Object = await _repositoryWrapper.PersonRepositoryWrapper.FindOne(id);

            if (null == Person_Object)
            {
                return NotFound();
            }
            else
            {
                PersonDto PersonDto_Object = Person_Object.Adapt<PersonDto>();
                return Ok(PersonDto_Object);
            }
        }

        // PUT: api/Persons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id,
                                                    [FromBody] PersonForUpdateDto PersonDto_Object,
                                                    string UserName = "No Name")
        {
            if (id != PersonDto_Object.PersonId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var PersonFromRepo = await _repositoryWrapper.PersonRepositoryWrapper.FindOne(id);

            if (null == PersonFromRepo)
            {
                return NotFound();
            }

            if (PersonFromRepo.CloneData<Person>(PersonDto_Object))
            {
                await _repositoryWrapper.PersonRepositoryWrapper.Update(PersonFromRepo);
            }

            return NoContent();
        }

        // POST: api/Persons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson([FromBody] PersonForSaveWithSchoolDto PersonDto_Object,
                                                            string UserName = "No Name")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person Person_Object = PersonDto_Object.Adapt<Person>();
            await _repositoryWrapper.PersonRepositoryWrapper.Create(Person_Object);

            return Ok(Person_Object.PersonID);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int CityId,
                                                      string UserName = "No Name")
        {
            _repositoryWrapper.PersonRepositoryWrapper.DisableLazyLoading();

            var PersonFromRepo = await _repositoryWrapper.PersonRepositoryWrapper.FindOne(CityId);

            if (null == PersonFromRepo)
            {
                return NotFound();
            }

            await _repositoryWrapper.PersonRepositoryWrapper.Delete(PersonFromRepo);

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return (null != _repositoryWrapper.PersonRepositoryWrapper.FindOne(id));
        }
    }
}
