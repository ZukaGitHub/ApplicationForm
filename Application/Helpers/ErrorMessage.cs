using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class ErrorMessage
    {
        public IEnumerable<string>? ErrorMessages { get; set; }
        public int StatusCode { get; set; }
    }
}
