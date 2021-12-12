using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DTO
{
    public class StudentForSaveDto : PersonDto
    {

    }

    public class StudentSaveWithSchoolDto : PersonForSaveDto
    {
        public virtual int SchoolID { get; set; }
    }

    public class StudentForUpdateDto : PersonForSaveWithSchoolDto
    {
        public int PersonId { get; set; }
    }

    public class StudentnDto : PersonForUpdateDto
    {
        public SchoolDtoNoPerson School { get; set; }
    }

    public class StudentDtoNoSchool : PersonForUpdateDto
    {

    }
}

