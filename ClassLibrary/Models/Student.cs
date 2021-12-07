using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClassLibrary.Enums;

namespace ClassLibrary.Models
{
    public class Student : Person
    {
        public string Class { get; set; }

        public StudentType StudentType { get; set; }

        public string StudentHatColor { get; set; }
    }
}
