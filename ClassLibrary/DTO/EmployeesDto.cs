using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DTO
{
    public class EmployeeForSaveDto
    {
        [Required]
        [MaxLength(50)]
        public string Salary { get; set; }
        [Required]
        public string EmployeeType { get; set; }

    }

    public class EmployeeForUpdateDto : EmployeeForSaveDto
    {
        public int PersonID { get; set; }
    }

    public class PersonDtoNoPerson : SchoolForUpdateDto
    {

    }

    public class EmployeeDto : PersonForUpdateDto
    {
        public List<PersonDtoNoSchool> Persons { get; set; }
    }
}