using ClassLibrary.Content;
using ClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DataManager
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DatabaseContext _repoContext;

        private IPersonRepository _PersonRepositoryWrapper;
        private ISchoolRepository _SchoolRepositoryWrapper;
        private IEmployeeRepository _EmployeesRepositoryWrapper;
        private IEUDStudentRepository _EUDStudentRepositoryWrapper;
        private IHTXStudentRepository _HTXStudentRepositoryWrapper;

        public RepositoryWrapper(DatabaseContext repositoryContext)
        {
            this._repoContext = repositoryContext;
        }

        public IPersonRepository PersonRepositoryWrapper
        {
            get
            {
                if (null == _PersonRepositoryWrapper)
                {
                    _PersonRepositoryWrapper = new PersonRepository(_repoContext);
                }

                return (_PersonRepositoryWrapper);
            }
        }

        public IEmployeeRepository EmployeeRepositoryWrapper
        {
            get
            {
                if (null == _EmployeesRepositoryWrapper)
                {
                    _EmployeesRepositoryWrapper = new EmployeeRepository(_repoContext);
                }

                return (_EmployeesRepositoryWrapper);
            }
        }

        public IEUDStudentRepository EUDStudentRepositoryWrapper
        {
            get
            {
                if (null == _EUDStudentRepositoryWrapper)
                {
                    _EUDStudentRepositoryWrapper = new EUDStudentRepository(_repoContext);
                }

                return (_EUDStudentRepositoryWrapper);
            }
        }

        public IHTXStudentRepository HTXStudentRepositoryWrapper
        {
            get
            {
                if (null == _HTXStudentRepositoryWrapper)
                {
                    _HTXStudentRepositoryWrapper = new HTXStudentRepository(_repoContext);
                }

                return (_HTXStudentRepositoryWrapper);
            }
        }


        public ISchoolRepository SchoolRepositoryWrapper
        {
            get
            {
                if (null == _SchoolRepositoryWrapper)
                {
                    _SchoolRepositoryWrapper = new SchoolRepository(_repoContext);
                }

                return (_SchoolRepositoryWrapper);
            }
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}

