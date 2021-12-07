using ClassLibrary.Content;
using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DataManager
{
    public class EUDStudentRepository : RepositoryBase<EUDStudent>, IEUDStudentRepository
    {
        private readonly DatabaseContext _context;

        public EUDStudentRepository(DatabaseContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}