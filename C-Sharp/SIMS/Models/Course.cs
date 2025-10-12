using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.Models
{
    public class Course : CourseBase
    {
        public override string GetCourseType()
        {
            return "Regular";
        }
    }
}
