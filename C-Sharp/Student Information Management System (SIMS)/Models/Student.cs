using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_Management_System__SIMS_.Models
{
    public class Student : Person
    {
        public string Email { get; set; }
        public string Course { get; set; }

    }
}
