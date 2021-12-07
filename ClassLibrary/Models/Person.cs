using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public abstract class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonID { get; set; }

        [Required]
        [MaxLength(50)]
        public string PersonName { get; set; }

        [ForeignKey("SchoolID")]
        public int SchoolID { get; set; }

        public virtual School School { get; set; }
    }
}
