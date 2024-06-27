using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Data.Models
{
    public class Candidate : Base
    {

        [Required]
        [StringLength(500)]
        public string Lastname { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Firstname { get; set; } = null!;

        [Required]
        [StringLength(500)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "The mobile number must be exactly 10 digits.")]
        public long? Mobile { get; set; }

        public string? CvBlob { get; set; }


        public ICollection<CandidateDegree> CandidateDegrees { get; set; } = [];

    }
}
