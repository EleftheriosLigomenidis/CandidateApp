using CandidateApp.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Business.Models
{
    public class Degree : Base
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; } = null!;

    }
}
