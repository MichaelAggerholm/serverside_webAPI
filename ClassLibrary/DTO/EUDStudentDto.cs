using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DTO
{
    public class EUDStudentForSaveDto : StudentForSaveDto
    {
        [Required]
        public string BusinessEducation { get; set; }

    }

    public class EUDStudentForUpdateDto : EUDStudentForSaveDto
    {
        public int PersonID { get; set; }
    }

    public class StudentDtoNoStudent : PersonForUpdateDto
    {

    }

    public class EUDStudentDto : PersonForUpdateDto
    {
        public List<PersonDtoNoSchool> Persons { get; set; }
    }
}