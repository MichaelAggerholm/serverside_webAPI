using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DTO
{
    public class EUDStudentForSaveDto
    {
        [Required]
        public string BusinessEducation { get; set; }

    }

    public class EUDStudentForUpdateDto : PersonForSaveDto
    {
        public int PersonID { get; set; }
    }

    public class StudentDtoNoStudent : PersonForUpdateDto
    {

    }
}