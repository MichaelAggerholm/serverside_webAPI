using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DTO
{
    public class PersonForSaveDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string PersonName { get; set; }

    }

    public class PersonForSaveWithSchoolDto : PersonForSaveDto
    {
        public virtual int SchoolID { get; set; }
    }

    public class PersonForUpdateDto : PersonForSaveWithSchoolDto
    {
        public int PersonId { get; set; }
    }

    public class PersonDto : PersonForUpdateDto
    {
        public SchoolDtoNoPerson School { get; set; }
    }

    public class PersonDtoNoSchool : PersonForUpdateDto
    {

    }
}

