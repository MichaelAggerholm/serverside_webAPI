using ClassLibrary.Content;
using ClassLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DataManager
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(DatabaseContext context) : base(context)
        {
            if (null == context)
            {
                throw new ArgumentNullException(nameof(context));
            }
            this.RepositoryContext.ChangeTracker.LazyLoadingEnabled = false;
        }

        public async Task<IEnumerable<Person>> GetAllPersons(bool IncludeRelations = false)
        {
            if (false == IncludeRelations)
            {
                var collection = await base.FindAll();
                return (collection);
            }
            else
            {
                var collection = await base.RepositoryContext.Persons.
                Include(co => co.School).ToListAsync();

                var collection1 = collection.OrderByDescending(c => c.School.SchoolName);
                return (collection1);
            }
        }

        public async Task<Person> GetPerson(int PersonId, bool IncludeRelations = false)
        {
            //if (false == IncludeRelations)
            //{
            //    var Person_Object = base.FindOne(CityId);
            //    return await (Person_Object);
            //}
            //else
            //{
            //    var Person_Object = await base.RepositoryContext.Persons.Include(co => co.school).
            //    FirstOrDefaultAsync();

            //    return (Person_Object);
            //}
            var Person_Object = base.FindOne(PersonId);
            return await (Person_Object);
        }

        public async Task<IEnumerable<Person>> GetPersonsWithShoolID(int SchoolID)
        {
            var collection = await base.FindByCondition(c => c.SchoolID == SchoolID);
            return (collection);
        }
    }
}
