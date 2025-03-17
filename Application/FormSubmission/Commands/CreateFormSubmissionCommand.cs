using Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FormSubmission.Commands
{
    public class CreateFormSubmissionCommand : IRequest<ResponseWrapper<int?>>
    {
        public Dictionary<string, object> Inputs { get; set; }
    }
}
