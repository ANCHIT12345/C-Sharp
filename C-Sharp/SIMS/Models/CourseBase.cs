using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.Models
{
    public abstract class CourseBase
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }

        public abstract string GetCourseType();
    }
}