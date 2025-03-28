﻿using Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FormSubmission.Queries
{
    public class GetFormSubmissionByIdQuery : IRequest<ResponseWrapper<FormSubmissionDTO>>
    {
        public int Id { get; set; }
    }
}
