using SIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.Interfaces
{
    public interface IReportGenerator
    {
        void GenerateReportCard(int studentId);
    }
}