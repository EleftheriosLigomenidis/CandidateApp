using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Data
{
    public enum DegreeGrade
    {
        [Description("A:Distrinction")]
        A,
        [Description("B:Honors")]
        B,
        [Description("C:Congratulations")]
        C,
        [Description("D:Base")]
        D
    }
}
