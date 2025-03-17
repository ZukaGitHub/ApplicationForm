using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enitites
{
    public class AdditionalProperties
    {
        public int Id { get; set; }
        public FormSubmission FormSubmission { get; set; }
        public int FormId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
