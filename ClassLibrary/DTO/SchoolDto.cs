using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DTO
{
    public class SchoolForSaveDto
    {
        [Required]
        [MaxLength(50)]
        public string SchoolName { get; set; }

    }

    public class SchoolForUpdateDto : SchoolForSaveDto
    {
        public int SchoolID { get; set; }
    }

    public class SchoolDtoNoPerson : SchoolForUpdateDto
    {

    }

    public class SchoolDto : SchoolForUpdateDto
    {
        public List<PersonDtoNoSchool> Persons { get; set; }
    }
}
