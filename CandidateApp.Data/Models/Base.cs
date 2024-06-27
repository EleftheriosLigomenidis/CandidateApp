using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Data.Models
{
    public  class Base
    {
        
        public  long Id { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
        public override string ToString()
        {
            var properties = GetType().GetProperties();

            // Build a string representation based on property values
            var sb = new StringBuilder();
            sb.Append(GetType().Name).Append(": ");

            foreach (var prop in properties)
            {
                var value = prop.GetValue(this);
                sb.Append(prop.Name).Append(" = ").Append(value).Append(", ");
            }

            // Remove the last ", " from the end of the string
            if (sb.Length > 2)
                sb.Length -= 2;

            return sb.ToString();
        }
    }
}
