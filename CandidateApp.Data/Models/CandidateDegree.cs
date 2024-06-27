using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Data.Models
{
    public class CandidateDegree
    {
        public long CandidateId { get; set; }
        public long DegreeId { get; set; }
        /// <summary>
        /// The grade the individual has earned 
        /// </summary>
        public DegreeGrade Grade { get; set; }
        public Candidate Candidate { get; set; }
        public Degree Degree { get; set; }
    }
}
