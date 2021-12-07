using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DTO
{
    public class HTXStudentForSaveDto
    {
        [Required]
        [MaxLength(50)]
        public string ITEducation { get; set; }
    }

    public class HTXStudentForUpdateDto : PersonForSaveDto
    {
        public int PersonlID { get; set; }
    }

    public class StudentDtoNoPerson : PersonForUpdateDto
    {

    }
}
