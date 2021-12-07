using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClassLibrary.Enums;

namespace ClassLibrary.Models
{
    public class Employee : Person
    {
        public double Salary { get; set; }

        public EmployeeType EmployeeType { get; set; }
    }
}
