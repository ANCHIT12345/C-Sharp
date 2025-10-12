using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.Models
{
    public class Student : Person
    {
        public int StudentID { get; set; }
        public int CourseID { get; set; }
    }
}
