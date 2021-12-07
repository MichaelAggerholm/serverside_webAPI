using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IRepositoryWrapper
    {
        IPersonRepository PersonRepositoryWrapper { get; }

        ISchoolRepository SchoolRepositoryWrapper { get; }

        IEmployeeRepository EmployeeRepositoryWrapper { get; }

        IHTXStudentRepository HTXStudentRepositoryWrapper { get; }

        IEUDStudentRepository EUDStudentRepositoryWrapper { get; }

        void Save();

    }
}
