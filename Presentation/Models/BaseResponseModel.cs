using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public record BaseResponseModel(bool Success, string Message, IEnumerable<string>? ErrorMessages, int StatusCode);
}
