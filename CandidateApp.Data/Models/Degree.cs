using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Data.Models
{
    public class Degree : Base
    {

        [Required]
        [StringLength(500)]
        public string Name { get; set; } = null!;

        public ICollection<CandidateDegree> CandidateDegrees { get; set; } = [];


        public bool IsUsed() => CandidateDegrees.Count > 0;
    }
}
