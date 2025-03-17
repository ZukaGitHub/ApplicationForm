using Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FormSubmission.Commands
{
    public class UpdateFormSubmissionCommand : IRequest<ResponseWrapper<FormSubmissionDTO>>
    {
        public int Id { get; set; }
        public Dictionary<string, object> UpdatedData{ get; set; }
    }
}
