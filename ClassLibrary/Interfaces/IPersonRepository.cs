using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IPersonRepository : IRepositoryBase<Person>
    {
        // De 3 metoder herunder er kun med for test formål. For at vise i 
        // CityController.cs hvordan man skal gøre for at få alle relationelle
        // data med, hvis man ikke har enabled lazy loading.
        Task<IEnumerable<Person>> GetAllPersons(bool IncludeRelations = false);

        Task<Person> GetPerson(int PersonId, bool IncludeRelations = false);

        Task<IEnumerable<Person>> GetPersonsWithShoolID(int CountryID);
    }
}
