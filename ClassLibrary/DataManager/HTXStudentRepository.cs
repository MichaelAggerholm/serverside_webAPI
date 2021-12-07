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
    public class HTXStudentRepository : RepositoryBase<HTXStudent>, IHTXStudentRepository
    {
        private readonly DatabaseContext _context;

        public HTXStudentRepository(DatabaseContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
