using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class ResponseWrapper<T>
    {
        public T? Result { get; set; }
        public bool Success => ErrorMessage == null;
        public ErrorMessage? ErrorMessage { get; set; }
    }
}
